using FluentAssert;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Officium.Plugins.Texts
{
    public class ExecutorTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new Executor(null).ShouldNotBeNull();
        }

        [Fact]
        public void CanExecute()
        {
            var http = new Mock<HttpRequest>();
            var logger = new Mock<ILogger>();
            new Executor(null).ExecuteRequest(http.Object, logger.Object);
        }

        [Fact]
        public void ExeutesAllPlugins()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("POST");
            var plugins = PostPlugins();

            new Executor(plugins).ExecuteRequest(http.Object, logger.Object);

            plugins.Cast<MockPlugin>()
                .Where(x => x.HttpRequest == null)
                .Count()
                .ShouldBeEqualTo(0);
        }

        /// -------------------------------------------
        ///        --------- HELPERS  --------
        /// -------------------------------------------

        private IFunctionPlugin[] PostPlugins()
        {
            var rtn = new[]
            {
                new MockPlugin(PluginStepOrder.AlwaysFirst),
                new MockPlugin(PluginStepOrder.BeforeAll),
                new MockPlugin(PluginStepOrder.BeforePost),
                new MockPlugin(PluginStepOrder.AfterPost),
                new MockPlugin(PluginStepOrder.AfterAll),
                new MockPlugin(PluginStepOrder.AlwaysLast),
            };

            return rtn;
        }

        class MockPlugin : IFunctionPlugin
        {
            public HttpRequest HttpRequest { get; private set; }
            public ILogger ILogger { get; private set; }
            public IPluginContext IPluginContext { get; private set; }
            public PluginStepOrder StepOrder { get; private set; }

            public IActionResult ActionResult { get; set; }

            public MockPlugin(PluginStepOrder pluginStepOrder)
            {
                StepOrder = pluginStepOrder;
            }
            
            public string Name
            {
                get { return StepOrder.ToString(); }
            }            

            public IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context)
            {
                HttpRequest = req;
                ILogger = logger;
                IPluginContext = context;
                return ActionResult;

            }


           
        }


    }
}
