using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssert;

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

            //var cmd = new OfficiumCommandFactory().BuildCommand(values);
            //cmd.CommandRequest.
        }


        private class MockCommand : ICommand
        {
            public ICommandRequest CommandRequest { get; set; }
            public ICommandResponse CommandResponse { get; set; }
        }
    }
}
