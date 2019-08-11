using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.ReqRes;
using System;

namespace Officium.Tools.Handlers
{
    internal class HandlerExecuter<T> : IHandler
        where T : IHandler
    {
        private readonly IServiceCollection services;
        private IHandler handler = null;
        public HandlerExecuter(IServiceCollection services)
        {
            this.services = services;
        }

        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            if (handler == null)
            {
                lock (this)
                {
                    handler = GetHandler(services);
                }
            }

            handler.HandleRequest(request, response);

        }

        private IHandler GetHandler(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var h = sp.GetService<T>();
            if (h == null)
            {
                throw new InvalidOperationException($"Unable to construct type of {typeof(T).FullName}");
            }
            return h;
        }
    }
}
