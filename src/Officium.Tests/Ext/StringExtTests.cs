using Officium.Ext;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssert;

namespace Officium.Tests.Ext
{
    public class StringExtTests
    {
        [Fact]
        public void CanFindValue()
        {
            "/v1/widget/id/33".ValueAfter("id").ShouldBeEqualTo("33");
        }

        [Fact]
        public void CanReturnEmpty()
        {
            "/v1/widget/id/33".ValueAfter("Notid").ShouldBeEqualTo(string.Empty);
        }

        [Fact]
        public void WillUseDefault()
        {
            var expected = "222";
            string.Empty.WithDefault(expected).ShouldBeEqualTo(expected);
        }
    }
}
