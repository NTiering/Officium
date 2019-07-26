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
            (new CommandFactory() == null).ShouldBeFalse();
        }

        [Fact]
        public void CanPopulateFromDictionary()
        {            

            var factory = new CommandFactory();
            var values = new Dictionary<string, string>
            {
                ["name"] = "rose tattoo"
            };
            factory.RegisterCommandType<MockCommand>(CommandRequestType.HttpPost, new Regex("."));

            var cmd = (MockCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.Name.ShouldBeEqualTo("rose tattoo");
        }

        [Fact]
        public void WillPopulateCommandRequestType()
        {
            var factory = new CommandFactory();
            var values = new Dictionary<string, string>
            {
                ["name"] = "rose tattoo"
            };
            factory.RegisterCommandType<MockCommand>(CommandRequestType.HttpPost, new Regex("."));
            var cmd = (MockCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.CommandRequestType.ShouldBeEqualTo(CommandRequestType.HttpPost);
        }

        [Fact]
        public void CanPopulateFromEmptyDictionary()
        {
            var factory = new CommandFactory();
            var values = new Dictionary<string, string>();            
            factory.RegisterCommandType<MockCommand>(CommandRequestType.HttpPost, new Regex("."));
            var cmd = (MockCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.Name.ShouldBeNull();
        }

        [Fact]
        public void CanPopulateFromNullDictionary()
        {
            var factory = new CommandFactory();
            factory.RegisterCommandType<MockCommand>(CommandRequestType.HttpPost, new Regex("."));
            var cmd = (MockCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", null);

            cmd.Name.ShouldBeNull();
        }

        [Fact]
        public void WillReturnNoMatchCommandIfNoMatch()
        {
            var factory = new CommandFactory();
            var cmd = (NoMatchCommand)factory.BuildCommand(CommandRequestType.HttpPost, "//path", null);

            cmd.ShouldNotBeNull();
        }

        [Fact]
        public void NoMatchCommandReturnsCorrectCommandRequestType()
        {
            var factory = new CommandFactory();
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
