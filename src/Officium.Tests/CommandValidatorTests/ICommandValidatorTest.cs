using Moq;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.CommandValidators;
using System.Collections.Generic;
using Xunit;

namespace Officium.Tests.CommandValidatorTests
{
    public class ICommandValidatorTest
    {
        [Fact]
        public void CanBeAdded()
        {
            var command = new MockCommand();
            var context = new MockCommandContext();

            var commandHandler = new Mock<ICommandHandler>();
            commandHandler.Setup(x => x.CanHandle(command,context)).Returns(true);
            var commandValidator = new Mock<ICommandValidator>();
            commandValidator.Setup(x => x.CanValidate(command,context)).Returns(false);

            var chf = new CommandHandlerFactory(new[] { commandHandler.Object }, new[] { commandValidator.Object },null);

            chf.GetCommandHandler(command,context)
                .Handle(command,context);

            commandHandler.Verify(x => x.Handle(command, context), Times.Once);
            commandHandler.Verify(x => x.CanHandle(command, context), Times.Once);
            commandValidator.Verify(x => x.CanValidate(command, context), Times.Once);
            commandValidator.Verify(x => x.Validate(command, context), Times.Never);

        }


        [Fact]
        public void CanBeCalled()
        {
            var command = new MockCommand();
            var context = new MockCommandContext();

            var commandHandler = new Mock<ICommandHandler>();
            commandHandler.Setup(x => x.CanHandle(command,context)).Returns(true);
            var commandValidator = new Mock<ICommandValidator>();
            commandValidator.Setup(x => x.CanValidate(command, context)).Returns(true);

            var chf = new CommandHandlerFactory(new[] { commandHandler.Object }, new[] { commandValidator.Object }, null);

            chf.GetCommandHandler(command, context)
                .Handle(command, context);

            commandHandler.Verify(x => x.Handle(command, context), Times.Once);
            commandHandler.Verify(x => x.CanHandle(command, context), Times.Once);
            commandValidator.Verify(x => x.CanValidate(command, context), Times.Once);
            commandValidator.Verify(x => x.Validate(command, context), Times.Once);

        }

        [Fact]
        public void CanStopHandler()
        {
            var command = new MockCommand();
            var context = new MockCommandContext();

            var commandHandler = new Mock<ICommandHandler>();
            commandHandler.Setup(x => x.CanHandle(command, context)).Returns(true);
            var commandValidator = new Mock<ICommandValidator>();
            commandValidator.Setup(x => x.CanValidate(command, context)).Returns(true);
            commandValidator.Setup(x => x.Validate(command, context)).Returns(new[] { new Mock<IValidationResult>().Object});

            var chf = new CommandHandlerFactory(new[] { commandHandler.Object }, new[] { commandValidator.Object },null);

            chf.GetCommandHandler(command, context)
                .Handle(command, context);

            commandHandler.Verify(x => x.Handle(command, context), Times.Never);
            commandHandler.Verify(x => x.CanHandle(command, context), Times.Once);
            commandValidator.Verify(x => x.CanValidate(command, context), Times.Once);
            commandValidator.Verify(x => x.Validate(command, context), Times.Once);

        }
        class MockCommand : ICommand
        {
            public string Id { get; set; }
        }

        class MockCommandContext : ICommandContext
        {
            public MockCommandContext()
            {
                CommandRequestType = CommandRequestType.HttpGet;
                CommandResponse = new CommandResponse();

            }
            public CommandRequestType CommandRequestType { get; set; }
            public ICommandResponse CommandResponse { get; set; }
            public Dictionary<string, string> Input { get; set; }
            public string RequestPath { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        }
    }
}
