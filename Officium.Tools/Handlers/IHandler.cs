using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Helpers;
using Officium.Tools.ReqRes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Tools.Handlers
{
    class Builder
    {
        private readonly List<IHandler> handlerWrappers = new List<IHandler>();
        private readonly IServiceCollection services;
        private static RouteMatcher routeMatcher = new RouteMatcher();
        public Builder(IServiceCollection services)
        {
            this.services = services;
            RegisterRequestHandler();
        }


        public Builder BeforeEveryRequest<T>()
            where T : IHandler
        {
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.BeforeEveryRequest, new HandlerExecuter<T>(services), AlwaysAction));
            return this;
        }

        public Builder AfterEveryRequest<T>()
            where T : IHandler
        {
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.AfterEveryRequest, new HandlerExecuter<T>(services), AlwaysAction));
            return this;
        }
        public Builder OnError<T>()
            where T : IHandler
        {
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnError, new HandlerExecuter<T>(services), AlwaysAction));
            return this;
        }

        public Builder OnNotHandled<T>()
            where T : IHandler
        {
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnNotHandled, new HandlerExecuter<T>(services), AlwaysAction));
            return this;
        }

        public Builder ValidateRequest<T>(Method method, string pathSelector, IHandler handler)
            where T : IHandler
        {
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.ValidateRequest, handler, MakeSelectorAction(method, pathSelector)));
            return this;

        }

        public Builder OnRequest<T>(Method method, string pathSelector, IHandler handler)
            where T : IHandler
        {
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnRequest, handler, MakeSelectorAction(method, pathSelector)));
            return this;

        }

        private void RegisterRequestHandler()
        {
            services.AddSingleton(GetRequestResolver);
        }

        private RequestResolver GetRequestResolver(IServiceProvider arg)
        {
            return new RequestResolver(handlerWrappers.ToArray());
        }

        private Func<RequestContext, ResponseContent, bool> MakeSelectorAction(Method method, string pathSelector)
        {
            return (req, res) =>
            {
                var rtn =
                    req.RequestMethod == method &&
                    routeMatcher.Matches(pathSelector, req.Path);
                return rtn;
            };
              
        }


        private static Func<RequestContext, ResponseContent, bool> AlwaysAction
        {
            get { return (req, res) => { return true; }; }
        }
    }

    class RequestResolver
    {
        private readonly IHandler[] handlers;

        public RequestResolver(IHandler[] handlers)
        {
            this.handlers = handlers;
        }

        public object Resolve()
        {
            return null;
        }
    }

    interface IHandler
    {
        void HandleRequest(RequestContext request, ResponseContent response);
    }

    interface IGatedHandler : IHandler
    {
        bool CanHandleRequest(RequestContext request, ResponseContent response);
    }

    interface IOrderedHandler : IHandler
    {
        HandlerOrder Order { get; }
    }

    class HandlerWrapper : IOrderedHandler, IGatedHandler
    {
        private readonly IHandler handler;
        private readonly Func<RequestContext, ResponseContent, bool> canHandleAction;

        public HandlerWrapper(HandlerOrder order, IHandler handler, Func<RequestContext, ResponseContent, bool> canHandleAction)
        {
            this.Order = order;
            this.handler = handler;
            this.canHandleAction = canHandleAction;
        }

        public HandlerOrder Order { get; private set; }
        public bool CanHandleRequest(RequestContext request, ResponseContent response) => canHandleAction(request, response);
        public void HandleRequest(RequestContext request, ResponseContent response) => handler.HandleRequest(request, response);
    }



    class HandlerExecuter<T> : IHandler
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
            return h;
        }
    }

    enum HandlerOrder
    {
        OnNotHandled = -100,
        OnError = -200,
        BeforeEveryRequest = 100,
        ValidateRequest = 200,
        OnRequest = 300,
        AfterEveryRequest = 400
    }

    public enum Method
    {
        GET, POST, PUT, DELETE
    }
}
