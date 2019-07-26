using Officium.Commands;
using System.Collections.Generic;
using Xunit;
using FluentAssert;
using System.Text.RegularExpressions;
using Officium.CommandHandlers;
using FunctionApp2.CommandHandlers;
using Moq;

namespace Officium.Tests.CommandHandler
{
    public class CommandHandlerFactoryTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            (new CommandHandlerFactory(null)).ShouldNotBeNull();
        }

        [Fact]
        public void NullsDontCauseExceptions()
        {
            new CommandHandlerFactory(null).GetCommandHandler(null);
        }

        [Fact]
        public void DefaultToNoMatchCommandHandler()
        {
            (new CommandHandlerFactory(null).GetCommandHandler(null) as NoMatchCommandHandler).ShouldNotBeNull();
        }

        [Fact]
        public void SelectsCorrectHandlers()
        {
            var handler = new Mock<ICommandHandler>();
            var command = new Mock<ICommand>().Object;
            handler.Setup(x => x.CanHandle(It.IsAny<ICommand>())).Returns(true);

            new CommandHandlerFactory(new[] {handler.Object}).GetCommandHandler(command).ShouldBeEqualTo(handler.Object);
        }

        
    }
}
