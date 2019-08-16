using System;
using System.Collections.Generic;
using FluentAssert;
using Officium.Tools.Request;
using Xunit;

namespace Officium.Tools.Tests.Request
{
    public class ValueExtractorTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new ValueExtractor().ShouldNotBeNull();
        }

        [Fact]
        public void CanFindDictionaryValues()
        {
            var dict = new Dictionary<string, string>
            {
                ["ID"] = "1",
                ["value"] = "some values"
            };
            string output = null;
            new ValueExtractor().TryGetValue(dict, "id", ref output).ShouldBeTrue();
            new ValueExtractor().TryGetValue(dict, "value", ref output).ShouldBeTrue();
        }
        [Fact]
        public void CanReturnDictionaryValues()
        {
            var dict = new Dictionary<string, string>
            {
                ["ID"] = "1",
                ["value"] = "some values"
            };
            string output = null;
            new ValueExtractor().TryGetValue(dict, "id", ref output);
            output.ShouldBeEqualTo(dict["ID"]);
        }

        [Fact]
        public void CanFindPathValues()
        {
            var path = "/api/v1/example/33";

            var dict = new Dictionary<string, int>
            {
                ["version"] = 2,
                ["id"] = 4
            };

            string output = null;
            new ValueExtractor().TryGetPathValue(dict, path, "Id", ref output).ShouldBeTrue();
            new ValueExtractor().TryGetPathValue(dict, path, "version", ref output).ShouldBeTrue();
        }

        [Fact]
        public void CanExtractPathValues()
        {
            var path = "/api/v1/example/33";

            var dict = new Dictionary<string, int>
            {
                ["version"] = 2,
                ["id"] = 4
            };

            string output = null;
            new ValueExtractor().TryGetPathValue(dict, path, "Id", ref output);
            output.ShouldBeEqualTo("33");
        }
    }
}
