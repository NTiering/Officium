using FluentAssert;
using System.Linq;
using System.Collections.Generic;
using Officium.Plugins;
using Xunit;
using Officium.Plugins.Helpers;

namespace Officium.Plugins.Tests
{
    public class PluginStepOrderToolTests
    {      

        [Fact]
        public void GetReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.GetSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.BeforeGet);
            steps[1].ShouldBeEqualTo(PluginStepOrder.OnGet);
            steps[2].ShouldBeEqualTo(PluginStepOrder.AfterGet);
        }

        [Fact]
        public void PostReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.PostSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.BeforePost);
            steps[1].ShouldBeEqualTo(PluginStepOrder.OnPost);
            steps[2].ShouldBeEqualTo(PluginStepOrder.AfterPost);
        }

        [Fact]
        public void PutReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.PutSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.BeforePut);
            steps[1].ShouldBeEqualTo(PluginStepOrder.OnPut);
            steps[2].ShouldBeEqualTo(PluginStepOrder.AfterPut);
        }

        [Fact]
        public void DeleteReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.DeleteSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.BeforeDelete);
            steps[1].ShouldBeEqualTo(PluginStepOrder.OnDelete);
            steps[2].ShouldBeEqualTo(PluginStepOrder.AfterDelete);
        }

        [Fact]
        public void BeforeReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.BeforeAllSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.AlwaysFirst);
            steps[1].ShouldBeEqualTo(PluginStepOrder.BeforeAll);
        }
        [Fact]
        public void AfterReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.AfterAllSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.AfterAll);
            steps[1].ShouldBeEqualTo(PluginStepOrder.AlwaysLast);
        }       
    }
}
