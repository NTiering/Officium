using Officium.Commands;
using System.Collections.Generic;
using Xunit;
using FluentAssert;
using System.Text.RegularExpressions;

namespace Officium.Tests.Commands
{
    public class OfficiumCommandFactoryTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            (new CommandFactory(true) == null).ShouldBeFalse();
        }

        [Fact]
        public void CanPopulateFromDictionary()
        {
            var context = new MockCommandContext();
            var factory = new CommandFactory(true);
            var values = new Dictionary<string, string>
            {
                ["name"] = "rose tattoo"
            };
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".");

            var cmd = (MockCommand)factory.BuildCommand(context, values);

            cmd.Name.ShouldBeEqualTo("rose tattoo");
        }

        
        [Fact]
        public void WillNotAddDuplicates()
        {
            var factory = new CommandFactory(true);
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".").ShouldBeTrue();
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".").ShouldBeFalse();
        }

        [Fact]
        public void CanPopulateFromEmptyDictionary()
        {
            var context = new MockCommandContext();
            var factory = new CommandFactory(true);
            var values = new Dictionary<string, string>();
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".");
            var cmd = (MockCommand)factory.BuildCommand(context , values);

            cmd.Name.ShouldBeNull();
        }

        [Fact]
        public void CanPopulateFromNullDictionary()
        {
            var context = new MockCommandContext();

            var factory = new CommandFactory(true);
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".");
            var cmd = (MockCommand)factory.BuildCommand(context, null);

            cmd.Name.ShouldBeNull();
        }

        [Fact]
        public void WillReturnNoMatchCommandIfNoMatch()
        {
            var context = new MockCommandContext();
            var factory = new CommandFactory(true);
            var cmd = (NoMatchCommand)factory.BuildCommand(context, null);

            cmd.ShouldNotBeNull();
        }
     
        private class MockCommandContext : ICommandContext
        {
            public MockCommandContext()
            {
                CommandRequestType = CommandRequestType.HttpPost;
                CommandResponse = new CommandResponse();
                RequestPath = ".";
            }
            public Dictionary<string, string> Input { get; set; }
            public CommandRequestType CommandRequestType { get; set; } 
            public ICommandResponse CommandResponse { get; set; }
            public string RequestPath { get; set; }
        }

        private class MockCommand : ICommand
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
