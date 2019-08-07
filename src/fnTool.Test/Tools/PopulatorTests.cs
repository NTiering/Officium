using Officium.Core.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssert;

namespace fnTool.Test.Tools
{
    public class PopulatorTests
    {
        //[Fact]
        //public void CanPopulate1()
        //{
        //    var mockItem =  new Populator().Populate<MockItem>("/v1/{id}", "/v1/33");
        //    mockItem.Id.ShouldBeEqualTo(33);
        //}

        //[Fact]
        //public void CanPopulate2()
        //{
        //    var mockItem = new MockItem();
        //    new Populator().Populate("/v1/{id}", "/v1/sss", mockItem);
        //    mockItem.Id.ShouldBeEqualTo(0);
        //}

        //[Fact]
        //public void CanPopulate3()
        //{
        //    var mockItem = new MockItem();
        //    new Populator().Populate("/v1/{ID}", "/v1/55", mockItem);
        //    mockItem.Id.ShouldBeEqualTo(55);
        //}

        //[Fact]
        //public void CanPopulate4()
        //{
        //    var mockItem = new MockItem();
        //    new Populator().Populate("/v1/{Due}", "/v1/2019-12-20", mockItem);
        //    mockItem.Due.ShouldBeEqualTo(new DateTime(2019,12,20));
        //}

        //[Fact]
        //public void CanPopulate5()
        //{
        //    var mockItem = new MockItem();
        //    var dict = new Dictionary<string, string>
        //    {
        //        ["id"] = "33"
        //    };
        //    new Populator().Populate(dict, mockItem);
        //    mockItem.Id.ShouldBeEqualTo(33);
        //}

        //[Fact]
        //public void CanPopulate6()
        //{
        //    var mockItem = new MockItem();
        //    var dict = new Dictionary<string, string>
        //    {
        //        ["due"] = "2019-12-20"
        //    };
        //    new Populator().Populate(dict, mockItem);
        //    mockItem.Due.ShouldBeEqualTo(new DateTime(2019, 12, 20));
        //}
    }

    class MockItem
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime Due { get; set; }

    }
}
