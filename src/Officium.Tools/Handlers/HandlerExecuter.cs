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

        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            if (_handler == null)
            {
                lock (this)
                {
                    _handler = GetHandler(_services);
                }
            }
            _handler.HandleRequest(request, response);
        }

        private IHandler GetHandler(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var hdlr = provider.GetService<T>();
            if (hdlr == null)
            {
                throw new InvalidOperationException($"Unable to construct type of {typeof(T).FullName}");
            }
            return hdlr;
        }
    }
}
