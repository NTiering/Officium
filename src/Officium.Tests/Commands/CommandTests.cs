using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssert;
using Officium.Commands;

namespace Officium.Tests.Commands
{
    public class CommandTests
    {
        [Fact]
        public void BaseCommandIsOfTypeICommand()
        {
            typeof(ICommand).IsAssignableFrom(typeof(BaseCommand)).ShouldBeTrue();
        }

        [Fact]
        public void BaseCommandIsAbstract()
        {
            typeof(BaseCommand).IsAbstract.ShouldBeTrue();
        }
    }
}
