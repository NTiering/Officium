using System;
using Xunit;
using Officium.Tools.Handlers;
using FluentAssert;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Request;
using Officium.Tools.Response;

namespace Officium.Tools.Tests.Startup
{
    public class PathParamExtractorTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new PathParamExtractor().ShouldNotBeNull();
        }

        [Fact]
        public void CanHandleAnEmptyString()
        {
            new PathParamExtractor().MakePathParams(string.Empty).ShouldNotBeNull();
        }

        [Fact]
        public void CanHandleANull()
        {
            new PathParamExtractor().MakePathParams(null).ShouldNotBeNull();
        }

        [Fact]
        public void CanExtractValues()
        {
            new PathParamExtractor().MakePathParams("/api/{id}/value")["id"].ShouldBeEqualTo(2);
        }
    }
}
