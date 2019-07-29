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

            var factory = new CommandFactory(true);
            var values = new Dictionary<string, string>
            {
                ["name"] = "rose tattoo"
            };
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".");

            var cmd = (MockCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.Name.ShouldBeEqualTo("rose tattoo");
        }

        [Fact]
        public void WillPopulateCommandRequestType()
        {
            var factory = new CommandFactory(true);
            var values = new Dictionary<string, string>
            {
                ["name"] = "rose tattoo"
            };
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".");
            var cmd = (MockCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.CommandRequestType.ShouldBeEqualTo(CommandRequestType.HttpPost);
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
            var factory = new CommandFactory(true);
            var values = new Dictionary<string, string>();            
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".");
            var cmd = (MockCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.Name.ShouldBeNull();
        }

        [Fact]
        public void CanPopulateFromNullDictionary()
        {
            var factory = new CommandFactory(true);
            factory.TryRegisterCommandType<MockCommand>(CommandRequestType.HttpPost, ".");
            var cmd = (MockCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", null);

            cmd.Name.ShouldBeNull();
        }

        [Fact]
        public void WillReturnNoMatchCommandIfNoMatch()
        {
            var factory = new CommandFactory(true);
            var cmd = (NoMatchCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", null);

            cmd.ShouldNotBeNull();
        }

        [Fact]
        public void NoMatchCommandReturnsCorrectCommandRequestType()
        {
            var factory = new CommandFactory(true);
            var cmd = (NoMatchCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", null);

            cmd.CommandRequestType.ShouldBeEqualTo(CommandRequestType.NoMatch);
        }


        private class MockCommand : ICommand
        {
            public CommandRequestType CommandRequestType { get; set; }
            public string Name { get; set; }
            public ICommandResponse CommandResponse { get; set; }
        }
    }
}
