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
            var cmd = (MockCommand) builder.BuildCommand(CommandRequestType.HttpPost, "//path", values);

            cmd.Name.ShouldBeEqualTo("rose tattoo");
        }


        private class MockCommand : ICommand
        {
            public string Name { get; set; }
            public ICommandResponse CommandResponse { get; set; }
        }
    }
}
