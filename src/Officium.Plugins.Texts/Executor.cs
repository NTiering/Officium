using FluentAssert;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Officium.Plugins.Tests
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
            new Executor(new IFunctionPlugin[0]).ExecuteRequest(http.Object, logger.Object);
        }

        [Fact]
        public void ExeutesAllPostPlugins()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("POST");
            var plugins = PostPlugins();
            
            new Executor(plugins).ExecuteRequest(http.Object, logger.Object);

            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AlwaysFirst).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.BeforeAll).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.BeforePost).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.OnPost).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterPost).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterAll).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AlwaysLast).HttpRequest.ShouldNotBeNull();

            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.OnGet).HttpRequest.ShouldBeNull();
        }

        [Fact]
        public void ExeutesAllPostPluginsInTheCorrectOrder()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("POST");
            var plugins = PostPlugins();
            var callCount = -1;

            new Executor(plugins).ExecuteRequest(http.Object, logger.Object, new MockContext());

            plugins.Cast<MockPlugin>().Where(x=>x.HttpRequest != null).ToList().ForEach(plugin => 
            {
                Assert.True(plugin.CallCount > callCount);
                callCount = plugin.CallCount.Value;
            });
        }


        [Fact]
        public void ExeutesAllGetPlugins()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("Get");
            var plugins = PostPlugins();

            new Executor(plugins).ExecuteRequest(http.Object, logger.Object);

            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AlwaysFirst).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.BeforeAll).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.BeforeGet).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.OnGet).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterGet).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterAll).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AlwaysLast).HttpRequest.ShouldNotBeNull();

            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterPost).HttpRequest.ShouldBeNull();
        }

        [Fact]
        public void ExeutesAllGetPluginsInTheCorrectOrder()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("Get");
            var plugins = PostPlugins();
            var callCount = -1;

            new Executor(plugins).ExecuteRequest(http.Object, logger.Object, new MockContext());

            plugins.Cast<MockPlugin>().Where(x => x.HttpRequest != null).ToList().ForEach(plugin =>
            {
                Assert.True(plugin.CallCount > callCount);
                callCount = plugin.CallCount.Value;
            });
        }

        [Fact]
        public void ExeutesAllDeletePlugins()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("Delete");
            var plugins = PostPlugins();

            new Executor(plugins).ExecuteRequest(http.Object, logger.Object);

            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AlwaysFirst).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.BeforeAll).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.BeforeDelete).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.OnDelete).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterDelete).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterAll).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AlwaysLast).HttpRequest.ShouldNotBeNull();

            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterPost).HttpRequest.ShouldBeNull();
        }

        [Fact]
        public void ExeutesAllDeletePluginsInTheCorrectOrder()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("Delete");
            var plugins = PostPlugins();
            var callCount = -1;

            new Executor(plugins).ExecuteRequest(http.Object, logger.Object, new MockContext());

            plugins.Cast<MockPlugin>().Where(x => x.HttpRequest != null).ToList().ForEach(plugin =>
            {
                Assert.True(plugin.CallCount > callCount);
                callCount = plugin.CallCount.Value;
            });
        }

        [Fact]
        public void ExeutesAllPutPlugins()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("Put");
            var plugins = PostPlugins();

            new Executor(plugins).ExecuteRequest(http.Object, logger.Object);

            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AlwaysFirst).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.BeforeAll).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.BeforePut).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.OnPut).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterPut).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterAll).HttpRequest.ShouldNotBeNull();
            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AlwaysLast).HttpRequest.ShouldNotBeNull();

            plugins.Cast<MockPlugin>().First(x => x.StepOrder == PluginStepOrder.AfterPost).HttpRequest.ShouldBeNull();
        }

        [Fact]
        public void ExeutesAllPutPluginsInTheCorrectOrder()
        {
            var logger = new Mock<ILogger>();
            var http = new Mock<HttpRequest>();
            http.Setup(x => x.Method).Returns("Put");
            var plugins = PostPlugins();
            var callCount = -1;

            new Executor(plugins).ExecuteRequest(http.Object, logger.Object, new MockContext());

            plugins.Cast<MockPlugin>().Where(x => x.HttpRequest != null).ToList().ForEach(plugin =>
            {
                Assert.True(plugin.CallCount > callCount);
                callCount = plugin.CallCount.Value;
            });
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
                new MockPlugin(PluginStepOrder.OnPost),
                new MockPlugin(PluginStepOrder.AfterPost),
                new MockPlugin(PluginStepOrder.BeforeGet),
                new MockPlugin(PluginStepOrder.OnGet),
                new MockPlugin(PluginStepOrder.AfterGet),
                new MockPlugin(PluginStepOrder.BeforeDelete),
                new MockPlugin(PluginStepOrder.OnDelete),
                new MockPlugin(PluginStepOrder.AfterDelete),
                new MockPlugin(PluginStepOrder.BeforePut),
                new MockPlugin(PluginStepOrder.OnPut),
                new MockPlugin(PluginStepOrder.AfterPut),
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
            
            public int? CallCount { get; set; }
            public string Name
            {
                get { return StepOrder.ToString(); }
            }            

            public IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context)
            {
                CallCount = ((MockContext)context)?.GetCount();
                HttpRequest = req;
                ILogger = logger;
                IPluginContext = context;
                return ActionResult;

            }
           
        }

        class MockContext : IPluginContext
        {
            int i; 
            public int GetCount()
            {
                return i++;
            }
        }


    }
}
