using FluentAssert;
using System.Linq;
using System.Collections.Generic;
using Officium.Plugins;
using Xunit;

namespace Officium.Plugins.Texts
{
    public class PluginStepOrderToolTests
    {
       

        [Fact]
        public void GetReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.GetSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.AlwaysFirst);
            steps[1].ShouldBeEqualTo(PluginStepOrder.BeforeAll);
            steps[2].ShouldBeEqualTo(PluginStepOrder.BeforeGet);
            steps[3].ShouldBeEqualTo(PluginStepOrder.OnGet);
            steps[4].ShouldBeEqualTo(PluginStepOrder.AfterGet);
            steps[5].ShouldBeEqualTo(PluginStepOrder.AfterAll);
            steps[6].ShouldBeEqualTo(PluginStepOrder.AlwaysLast);
        }

        [Fact]
        public void PostReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.PostSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.AlwaysFirst);
            steps[1].ShouldBeEqualTo(PluginStepOrder.BeforeAll);
            steps[2].ShouldBeEqualTo(PluginStepOrder.BeforePost);
            steps[3].ShouldBeEqualTo(PluginStepOrder.OnPost);
            steps[4].ShouldBeEqualTo(PluginStepOrder.AfterPost);
            steps[5].ShouldBeEqualTo(PluginStepOrder.AfterAll);
            steps[6].ShouldBeEqualTo(PluginStepOrder.AlwaysLast);
        }

        [Fact]
        public void PutReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.PutSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.AlwaysFirst);
            steps[1].ShouldBeEqualTo(PluginStepOrder.BeforeAll);
            steps[2].ShouldBeEqualTo(PluginStepOrder.BeforePut);
            steps[3].ShouldBeEqualTo(PluginStepOrder.OnPut);
            steps[4].ShouldBeEqualTo(PluginStepOrder.AfterPut);
            steps[5].ShouldBeEqualTo(PluginStepOrder.AfterAll);
            steps[6].ShouldBeEqualTo(PluginStepOrder.AlwaysLast);
        }

        [Fact]
        public void DeleteReturnsCorrectValues()
        {
            var steps = PluginStepOrderTool.Instance.DeleteSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.AlwaysFirst);
            steps[1].ShouldBeEqualTo(PluginStepOrder.BeforeAll);
            steps[2].ShouldBeEqualTo(PluginStepOrder.BeforeDelete);
            steps[3].ShouldBeEqualTo(PluginStepOrder.OnDelete);
            steps[4].ShouldBeEqualTo(PluginStepOrder.AfterDelete);
            steps[5].ShouldBeEqualTo(PluginStepOrder.AfterAll);
            steps[6].ShouldBeEqualTo(PluginStepOrder.AlwaysLast);
        }

        [Fact]
        public void DeleteReturnsCorrectValuesTwice()
        {
            var steps = PluginStepOrderTool.Instance.DeleteSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.AlwaysFirst);
            steps[1].ShouldBeEqualTo(PluginStepOrder.BeforeAll);
            steps[2].ShouldBeEqualTo(PluginStepOrder.BeforeDelete);
            steps[3].ShouldBeEqualTo(PluginStepOrder.OnDelete);
            steps[4].ShouldBeEqualTo(PluginStepOrder.AfterDelete);
            steps[5].ShouldBeEqualTo(PluginStepOrder.AfterAll);
            steps[6].ShouldBeEqualTo(PluginStepOrder.AlwaysLast);
        }
    }
}
