using System;
using Officium.Plugins;
using FluentAssert;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Officium.Plugins.Texts
{
    public class DetectorTests
    {
        [Fact]
        public void CanBeConstucted()
        {
            new Detector().ShouldNotBeNull();
        }

        [Fact]
        public void CanDetectPlugins()
        {
            // arrange 
            var foundTypes = new List<Type>();
            
            // act  
            new Detector().Detect((Type type) => foundTypes.Add(type));

            // assert 
            Assert.Contains(foundTypes, (x) => { return x == typeof(MockPlugin); });
        }

        class MockPlugin : IFunctionPlugin
        {
            public PluginStepOrder StepOrder => throw new NotImplementedException();

            public IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context)
            {
                throw new NotImplementedException();
            }
        }
    }

    
}
