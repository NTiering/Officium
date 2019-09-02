using System;
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

        [Fact]
        public void AfterEveryRequestRegistersHandler()
        {
            var serviceCollection = new Mock<IServiceCollection>();
            serviceCollection.Setup(x => x.Contains(It.IsAny<ServiceDescriptor>())).Returns(false);
            using (var builder = new Builder(serviceCollection.Object))
            {
                builder.AfterEveryRequest<MockHandler>();
            }
        }

        [Fact]
        public void OnErrorRegistersHandler()
        {
            var serviceCollection = new Mock<IServiceCollection>();
            serviceCollection.Setup(x => x.Contains(It.IsAny<ServiceDescriptor>())).Returns(false);
            using (var builder = new Builder(serviceCollection.Object))
            {
                builder.OnError<MockHandler>();
            }
        }

        [Fact]
        public void OnRequestRegistersHandler()
        {
            var serviceCollection = new Mock<IServiceCollection>();
            serviceCollection.Setup(x => x.Contains(It.IsAny<ServiceDescriptor>())).Returns(false);
            using (var builder = new Builder(serviceCollection.Object))
            {
                builder.OnRequest<MockHandler>(RequestMethod.NOTMAPPED,"");
            }
        }      

       

        class MockHandler : IHandler
        {
            public static bool OnStartupWasCalled { get; set; }
            public IHandlerStartupContext HandlerStartupContext { get; private set; }
            public IRequestContext Request { get; private set; }
            public IResponseContent Response { get; private set; }
            public int CallCount { get; private set; }
            public void HandleRequest(IRequestContext request, IResponseContent response)
            {
                Request = request;
                Response = response;
                CallCount++;
            }

            public void OnStartup(IHandlerStartupContext context)
            {
                OnStartupWasCalled = true;
                HandlerStartupContext = context;
            }
        }
    }
}
