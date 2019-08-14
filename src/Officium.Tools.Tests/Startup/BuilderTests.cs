using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Officium.Tools.Handlers;
using FluentAssert;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Request;
using Officium.Tools.Response;

namespace Officium.Tools.Tests.Startup
{
    
    public class BuilderTests
    {
        [Fact]
        public void IsDisposible()
        {
            ((IDisposable)new Builder(null)).ShouldNotBeNull();
        }

        [Fact]
        public void BeforeEveryRequestRegistersHandler()
        {
            var serviceCollection = new Mock<IServiceCollection>();
            serviceCollection.Setup(x => x.Contains(It.IsAny<ServiceDescriptor>())).Returns(false);
            using (var builder = new Builder(serviceCollection.Object))
            {                
                builder.BeforeEveryRequest<MockHandler>();
            }

        }

        class MockHandler : IHandler
        {
            public RequestContext Request { get; private set; }
            public ResponseContent Response { get; private set; }
            public int CallCount { get; private set; }
            public void HandleRequest(RequestContext request, ResponseContent response)
            {
                Request = request;
                Response = response;
                CallCount++;
            }
        }
    }
}
