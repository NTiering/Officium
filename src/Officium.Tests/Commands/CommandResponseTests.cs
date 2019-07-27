using FluentAssert;
using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Officium.Tests.Commands
{
    public class CommandResponseTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new CommandResponse().ShouldNotBeNull();
        }

        [Fact]
        public void CanAddValues()
        {
            var response = new CommandResponse();
            response.AddValue("key", "value");
        }

        [Fact]
        public void CannotAddDuplicateValue()
        {
            Exception ex = null;
            try
            {
                var response = new CommandResponse();
                response.AddValue("key", "value");
                response.AddValue("key", "value");
            }
            catch (Exception e)
            {
                ex = e;
            }

            ex.ShouldNotBeNull();
        }

        [Fact]
        public void CanAddDuplicateValue()
        {
            Exception ex = null;
            try
            {
                var response = new CommandResponse();
                response.AddValue("key", "value");
                response.AddValue("key", "value", true);
            }
            catch (Exception e)
            {
                ex = e;
            }

            ex.ShouldBeNull();
        }

        [Fact]
        public void HasValueWorks()
        {
            var response = new CommandResponse();
            response.AddValue("key", "value");
            response.HasValue("key").ShouldBeTrue();
            response.HasValue("value").ShouldBeFalse();
        }

        [Fact]
        public void ValuesIsUpdated()
        {
            var response = new CommandResponse();
            response.AddValue("key", "value");
            response.Values["key"].ShouldBeEqualTo("value");
        }

        [Fact]
        public void ValuesDontUpdateOriginal()
        {
            var response = new CommandResponse();
            response.AddValue("key", "value");
            response.Values["key"] = "new value";
            response.Values["key"].ShouldBeEqualTo("value");
        }
    }
}
