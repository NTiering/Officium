using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;
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
            (new OfficiumCommandFactory() == null).ShouldBeFalse();
        }

        [Fact]
        public void CanPopulateFromDictionary()
        {
            var values = new Dictionary<string, string>();
            values["name"] = "rose tattoo";

            var builder = new OfficiumCommandFactory();
            builder.RegisterCommandType<MockCommand>(CommandRequestType.HttpPost, new Regex("."));
            var cmd = (MockCommand)builder.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.Name.ShouldBeEqualTo("rose tattoo");
        }

        [Fact]
        public void WillPopulateCommandRequestType()
        {
            var values = new Dictionary<string, string>();
            values["name"] = "rose tattoo";

            var builder = new OfficiumCommandFactory();
            builder.RegisterCommandType<MockCommand>(CommandRequestType.HttpPost, new Regex("."));
            var cmd = (MockCommand)builder.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.CommandRequestType.ShouldBeEqualTo(CommandRequestType.HttpPost);
        }

        [Fact]
        public void CanPopulateFromEmptyDictionary()
        {
            var values = new Dictionary<string, string>();

            var builder = new OfficiumCommandFactory();
            builder.RegisterCommandType<MockCommand>(CommandRequestType.HttpPost, new Regex("."));
            var cmd = (MockCommand)builder.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.Name.ShouldBeNull();
        }

        [Fact]
        public void CanPopulateFromNullDictionary()
        {
            var builder = new OfficiumCommandFactory();
            builder.RegisterCommandType<MockCommand>(CommandRequestType.HttpPost, new Regex("."));
            var cmd = (MockCommand)builder.BuildCommand(CommandRequestType.HttpPost, "//path", null);

            cmd.Name.ShouldBeNull();
        }

        [Fact]
        public void WillReturnNoMatchCommandIfNoMatch()
        {
            var builder = new OfficiumCommandFactory();
            var cmd = (NoMatchCommand)builder.BuildCommand(CommandRequestType.HttpPost, "//path", null);

            cmd.ShouldNotBeNull();
        }

        [Fact]
        public void NoMatchCommandReturnsCorrectCommandRequestType()
        {
            var builder = new OfficiumCommandFactory();
            var cmd = (NoMatchCommand)builder.BuildCommand(CommandRequestType.HttpPost, "//path", null);

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
