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
        public void CanBeConstructed()
        {
            new PluginStepOrderTool().ShouldNotBeNull();
        }

        [Fact]
        public void GetReturnsCorrectValues()
        {
            var steps = new PluginStepOrderTool().GetSteps.ToArray();
            steps[0].ShouldBeEqualTo(PluginStepOrder.AlwaysFirst);
            steps[1].ShouldBeEqualTo(PluginStepOrder.BeforeAll);
            steps[2].ShouldBeEqualTo(PluginStepOrder.BeforeGet);
            steps[3].ShouldBeEqualTo(PluginStepOrder.OnGet);
            steps[4].ShouldBeEqualTo(PluginStepOrder.AfterGet);           
            steps[5].ShouldBeEqualTo(PluginStepOrder.AfterAll);
            steps[6].ShouldBeEqualTo(PluginStepOrder.AlwaysAfter);
        }
    }
}
