namespace Officium.Tools.Handlers
{
    using Microsoft.Extensions.DependencyInjection;
    using Officium.Tools.Request;
    using Officium.Tools.Response;
    using System;
    internal class HandlerExecuter<T> : IHandler
        where T : IHandler
    {
        private readonly IServiceCollection _services;
        private IHandler _handler = null;

        public HandlerExecuter(IServiceCollection services)
        {
            _services = services;
        }

        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            GetHandler(_services).HandleRequest(request, response);
        }

        private IHandler GetHandler(IServiceCollection services)
        {
            if (_handler == null)
            {
                lock (this)
                {
                    var provider = services.BuildServiceProvider();
                    var newHandler = provider.GetService<T>();
                    if (newHandler == null)
                    {
                        throw new InvalidOperationException($"Unable to construct type of {typeof(T).FullName}");
                    }
                    _handler = newHandler;
                }
            }

            return _handler;
        }
    }
}
