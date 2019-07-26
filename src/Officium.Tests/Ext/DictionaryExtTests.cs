using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using Officium.Ext;
using Xunit;
using FluentAssert;
using Officium.Commands;

namespace Officium.Tests.Ext
{
    public class DictionaryExtTests
    {
        [Fact]
        public void CanAddQueryCollection()
        {
            // arrange 
            var expectedValue = new StringValues("rose tattoo");
            var expectedKey = "name";
            var d = new Dictionary<string, StringValues>
            {
                [expectedKey] = expectedValue
            };
            var queryCollection = new QueryCollection(d);

            // act 
            var dict = new Dictionary<string, string>();
            dict.AddRange(queryCollection);

            // assert
            dict[expectedKey].ShouldBeEqualTo(expectedValue.ToString());
        }
        [Fact]
        public void CanAddDictionary()
        {
            // arrange 
            var expectedValue = "rose tattoo";
            var expectedKey = "name";
            var d = new Dictionary<string,string>
            {
                [expectedKey] = expectedValue
            };          

            // act 
            var dict = new Dictionary<string, string>();
            dict.AddRange(d);

            // assert
            dict[expectedKey].ShouldBeEqualTo(expectedValue);
        }

        [Fact]
        public void CanCastToObject()
        {
            // arrange 
            var dict = new Dictionary<string, string>
            {
                ["Number"] = "22",
                ["name"] = "Rose"
            };

            // act 
            var result = dict.ToObject(typeof(TestObject)) as TestObject;

            result.Name.ShouldBeEqualTo("Rose");
            result.Number.ShouldBeEqualTo(22);
        }

        class TestObject : ICommand
        {
            public int Number { get; set; }
            public string Name { get; set; }
            public ICommandResponse CommandResponse { get; set; }
            public CommandRequestType CommandRequestType { get; set; }
        }
    }
}
