using FluentAssert;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Officium.Plugins.Texts
{
    public class ExecuteCollectionBuilderTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new ExecuteCollectionBuilder().ShouldNotBeNull();
        }



        class MockPlugin : IFunctionPlugin
        {
            public PluginStepOrder StepOrder { get; set; }
        }

    }
}
