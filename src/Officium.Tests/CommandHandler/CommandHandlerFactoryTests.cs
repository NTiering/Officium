using Officium.Commands;
using Xunit;
using FluentAssert;
using Officium.CommandHandlers;
using Moq;
using Officium.CommandValidators;

namespace Officium.Tests.CommandHandler
{
    public class CommandHandlerFactoryTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            (new CommandHandlerFactory(null,null,null)).ShouldNotBeNull();
        }

        [Fact]
        public void NullsDontCauseExceptions()
        {
            new CommandHandlerFactory(null,null,null).GetCommandHandler(null);
        }       

        [Fact]
        public void SelectsCorrectHandlers()
        {
            var command = new Mock<ICommand>();
            command.Setup(x => x.CommandResponse).Returns(new CommandResponse());
            var handler = new Mock<ICommandHandler>();
            handler.Setup(x => x.CanHandle(It.IsAny<ICommand>())).Returns(true);

            new CommandHandlerFactory(new[] { handler.Object }, null, null).GetCommandHandler(command.Object).Handle(command.Object);

            handler.Verify(x => x.Handle(command.Object), Times.Once);
        }

        [Fact]
        public void SelectsCorrectValidator()
        {
            var handler = new Mock<ICommandHandler>();
            handler.Setup(x => x.CanHandle(It.IsAny<ICommand>())).Returns(true);
            var command = new Mock<ICommand>();
            command.Setup(x => x.CommandResponse).Returns(new CommandResponse());

            var validator = new Mock<ICommandValidator>();
            validator.Setup(x => x.CanValidate(It.IsAny<ICommand>())).Returns(true);

            new CommandHandlerFactory(new[] { handler.Object }, new[] { validator.Object },null).GetCommandHandler(command.Object).Handle(command.Object);

            validator.Verify(x => x.Validate(command.Object), Times.Once);
        }


    }
}
