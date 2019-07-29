using Moq;
using Officium.CommandFilters;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.CommandValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Officium.Tests.CommandFilterTests
{
    public class CommandFilterTest
    {
        [Fact]
        public void CallsBeforeFilter()
        {
            var command = new MockCommand();

            var filter = new Mock<ICommandFilter>();
            filter.Setup(x => x.CanFilter(command)).Returns(true);

            new ValidatingCommandHandler(new List<ICommandValidator>(), null, new[] { filter.Object }.ToList()).Handle(command);

            filter.Verify(x => x.BeforeHandleEvent(command), Times.Once);
        }

        [Fact]
        public void CallsAfterFilter()
        {
            var command = new MockCommand();

            var filter = new Mock<ICommandFilter>();
            filter.Setup(x => x.CanFilter(command)).Returns(true);

            new ValidatingCommandHandler(new List<ICommandValidator>(), null, new[] { filter.Object }.ToList()).Handle(command);

            filter.Verify(x => x.AfterHandleEvent(command), Times.Once);
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
