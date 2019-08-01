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
using System.Linq;

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

        [Fact]
        public void CanMakePaginatioRequestObject()
        {
            // arrange 
            var dict = new Dictionary<string, string>
            {
                ["PageNum"] = "22",
                ["PageSize"] = "77"
            };

            // act 
            var result = dict.ToPaginationRequest();

            result.PageNum.ShouldBeEqualTo(22);
            result.PageSize.ShouldBeEqualTo(77);
        }

        [Fact]
        public void CanMakePaginatioRequestObjectWithBadData()
        {
            // arrange 
            var dict = new Dictionary<string, string>
            {
                ["PageNum"] = "wererww",
                ["PageSize"] = "asads"
            };

            // act 
            var result = dict.ToPaginationRequest();

            result.PageNum.ShouldBeEqualTo(0);
            result.PageSize.ShouldBeEqualTo(25);
        }

        [Fact]
        public void CanPaginate()
        {
            // arrange 
            var dict = new Dictionary<string, string>
            {
                ["PageNum"] = "0",
                ["PageSize"] = "5"
            };
            var paginationRequest = dict.ToPaginationRequest();
            var arr = Enumerable.Range(1, 100);

            // act 
            var result = arr.Paginate(paginationRequest).Last();

            result.ShouldBeEqualTo(5);
        }

        [Fact]
        public void CanPaginateQueryable()
        {
            // arrange 
            var dict = new Dictionary<string, string>
            {
                ["PageNum"] = "0",
                ["PageSize"] = "5"
            };
            var paginationRequest = dict.ToPaginationRequest();
            var arr = Enumerable.Range(1, 100).AsQueryable();

            // act 
            var result = arr.Paginate(paginationRequest).Last();

            result.ShouldBeEqualTo(5);
        }

        [Fact]
        public void CanPaginateQueryableWithPages()
        {
            // arrange 
            var dict = new Dictionary<string, string>
            {
                ["PageNum"] = "1",
                ["PageSize"] = "5"
            };
            var paginationRequest = dict.ToPaginationRequest();
            var arr = Enumerable.Range(1, 100).AsQueryable();

            // act 
            var result = arr.Paginate(paginationRequest).Last();

            result.ShouldBeEqualTo(10);
        }


        class TestObject : ICommand
        {
            public string Id { get; set; }
            public int Number { get; set; }
            public string Name { get; set; }
            public ICommandResponse CommandResponse { get; set; }
            public CommandRequestType CommandRequestType { get; set; }
            public string RequestPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
    }
}
