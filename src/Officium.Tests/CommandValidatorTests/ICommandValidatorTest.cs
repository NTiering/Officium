using Moq;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.CommandValidators;
using Xunit;

namespace Officium.Tests.CommandValidatorTests
{
    public class ICommandValidatorTest
    {
        [Fact]
        public void CanBeAdded()
        {
            var command = new MockCommand();
            var commandHandler = new Mock<ICommandHandler>();
            commandHandler.Setup(x => x.CanHandle(It.IsAny<ICommand>())).Returns(true);
            var commandValidator = new Mock<ICommandValidator>();
            commandValidator.Setup(x => x.CanValidate(It.IsAny<ICommand>())).Returns(false);

            var chf = new CommandHandlerFactory(new[] { commandHandler.Object }, new[] { commandValidator.Object });

            chf.GetCommandHandler(command)
                .Handle(command);

            commandHandler.Verify(x => x.Handle(command), Times.Once);
            commandHandler.Verify(x => x.CanHandle(command), Times.Once);
            commandValidator.Verify(x => x.CanValidate(command), Times.Once);
            commandValidator.Verify(x => x.Validate(command), Times.Never);

        }


        [Fact]
        public void CanBeCalled()
        {
            var command = new MockCommand();
            var commandHandler = new Mock<ICommandHandler>();
            commandHandler.Setup(x => x.CanHandle(It.IsAny<ICommand>())).Returns(true);
            var commandValidator = new Mock<ICommandValidator>();
            commandValidator.Setup(x => x.CanValidate(It.IsAny<ICommand>())).Returns(true);

            var chf = new CommandHandlerFactory(new[] { commandHandler.Object }, new[] { commandValidator.Object });

            chf.GetCommandHandler(command)
                .Handle(command);

            commandHandler.Verify(x => x.Handle(command), Times.Once);
            commandHandler.Verify(x => x.CanHandle(command), Times.Once);
            commandValidator.Verify(x => x.CanValidate(command), Times.Once);
            commandValidator.Verify(x => x.Validate(command), Times.Once);

        }

        [Fact]
        public void CanStopHandler()
        {
            var command = new MockCommand();
            var commandHandler = new Mock<ICommandHandler>();
            commandHandler.Setup(x => x.CanHandle(It.IsAny<ICommand>())).Returns(true);
            var commandValidator = new Mock<ICommandValidator>();
            commandValidator.Setup(x => x.CanValidate(It.IsAny<ICommand>())).Returns(true);
            commandValidator.Setup(x => x.Validate(It.IsAny<ICommand>())).Returns(new[] { new Mock<IValidationResult>().Object});

            var chf = new CommandHandlerFactory(new[] { commandHandler.Object }, new[] { commandValidator.Object });

            chf.GetCommandHandler(command)
                .Handle(command);

            commandHandler.Verify(x => x.Handle(command), Times.Never);
            commandHandler.Verify(x => x.CanHandle(command), Times.Once);
            commandValidator.Verify(x => x.CanValidate(command), Times.Once);
            commandValidator.Verify(x => x.Validate(command), Times.Once);

        }
        class MockCommand : ICommand
        {
            public MockCommand()
            {
                CommandRequestType = CommandRequestType.HttpGet;
                CommandResponse = new CommandResponse();

            }
            public CommandRequestType CommandRequestType { get; set; }
            public ICommandResponse CommandResponse { get; set; }
        }
    }
}
