namespace Officium.Tools.Handlers
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    internal class HandlerStarter : IStartupHandler
    {
        private readonly IServiceCollection _services;
        private readonly Type _handlerType;
        private IStartupHandler _handler = null;

        public HandlerStarter(IServiceCollection services, Type handlerType)
        {
            _services = services;
            _handlerType = handlerType;
        }       

        public void OnStartup(IHandlerStartupContext context)
        {
            GetHandler(_services).OnStartup(context);
        }

        private IStartupHandler GetHandler(IServiceCollection services)
        {
            if (_handler == null)
            {
                lock (this)
                {
                    var provider = services.BuildServiceProvider();
                    var newHandler = provider.GetService(_handlerType) as IStartupHandler;
                    if (newHandler == null)
                    {
                        throw new InvalidOperationException($"Unable to construct type of {_handlerType.FullName}");
                    }
                    _handler = newHandler;
                }
            }

            return _handler;
        }
    }
}
