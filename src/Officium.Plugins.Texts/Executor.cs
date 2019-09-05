using FluentAssert;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Officium.Plugins.Texts
{
    public class ExecutorTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new Executor().ShouldNotBeNull();
        }

        [Fact]
        public void CanExecute()
        {
            var http = new Mock<HttpRequest>();
            var logger = new Mock<ILogger>();
            new Executor().ExecuteRequest(http.Object, logger.Object);
        }


       

    }
}
