using Officium.Tools.Helpers;
using FluentAssert;
using Xunit;

namespace Officium.Tools.Tests.Helpers
{
    public class RouteMatcherTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new RouteMatcher().ShouldNotBeNull();
        }

        [Fact]
        public void MatchesTrue()
        {
            new RouteMatcher().Matches("/id", "/id").ShouldBeTrue();
            new RouteMatcher().Matches("/id", "id").ShouldBeTrue();
            new RouteMatcher().Matches("/id", "iD").ShouldBeTrue();
            new RouteMatcher().Matches("/id/{name}", "iD/somename").ShouldBeTrue();
            new RouteMatcher().Matches("/id/{name}/32", "iD/somename/32").ShouldBeTrue();
            new RouteMatcher().Matches("{name}/32", "somename/32").ShouldBeTrue();
        }

        [Fact]
        public void Matchesfalse()
        {
            new RouteMatcher().Matches("/id", "api/id").ShouldBeFalse();
            new RouteMatcher().Matches("/id", "/id/33").ShouldBeFalse();

        }
    }
}
