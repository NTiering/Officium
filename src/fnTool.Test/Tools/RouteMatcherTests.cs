using Officium.Core.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssert;

namespace fnTool.Test.Tools
{
    public class RouteMatcherTests
    {
        [Fact]
        public void Matches()
        {
            new RouteMatcher().Matches("v1/", "v1").ShouldBeTrue("0");

        }

        [Fact]
        public void MatchesTest1()
        {
            new RouteMatcher().Matches("/v1/", "v1").ShouldBeTrue("1");

        }

        [Fact]
        public void MatchesTest2()
        {
            new RouteMatcher().Matches("/v1/", "v1/").ShouldBeTrue("2");

        }

        [Fact]
        public void MatchesTest3()
        {
            new RouteMatcher().Matches("/V1/", "v1").ShouldBeTrue("3");

        }

        [Fact]
        public void MatchesTest4()
        {
            new RouteMatcher().Matches("/V1/item", "v1/.").ShouldBeTrue("4");
 
        }

        [Fact]
        public void MatchesTest5()
        {
            new RouteMatcher().Matches("/V1/item/42", "v1/item/{id}").ShouldBeTrue("5");
        }

        [Fact]
        public void MatchesTest6()
        {
            new RouteMatcher().Matches("/v1/item/42", "v1/item/").ShouldBeFalse("6");
        }        
    }
}
