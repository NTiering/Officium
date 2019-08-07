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
            new RouteMatcher().Matches("v1/", "v1").ShouldBeTrue();

        }

        [Fact]
        public void MatchesTest1()
        {
            new RouteMatcher().Matches("/v1/", "v1").ShouldBeTrue();

        }

        [Fact]
        public void MatchesTest2()
        {
            new RouteMatcher().Matches("/v1/", "v1/").ShouldBeTrue();

        }

        [Fact]
        public void MatchesTest3()
        {
            new RouteMatcher().Matches("/V1/", "v1").ShouldBeTrue();

        }

        [Fact]
        public void MatchesTest4()
        {
            new RouteMatcher().Matches("/V1/item", "v1/.").ShouldBeTrue();
 
        }

        [Fact]
        public void MatchesTest5()
        {
            new RouteMatcher().Matches("/V1/item/42", "v1/item/{id}").ShouldBeTrue();
        }

        [Fact]
        public void MatchesTest6()
        {
            new RouteMatcher().Matches("/v1/item/42", "v1/item/").ShouldBeFalse();
        }        
    }
}
