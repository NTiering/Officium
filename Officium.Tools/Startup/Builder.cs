using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Helpers;
using Officium.Tools.ReqRes;
using System;
using System.Collections.Generic;

namespace Officium.Tools.Handlers
{
    public class Builder : IDisposable
    {
        private readonly List<IHandlerWrapper> handlerWrappers = new List<IHandlerWrapper>();
        private readonly IServiceCollection services;
        private readonly static RouteMatcher routeMatcher = new RouteMatcher();
        public Builder(IServiceCollection services)
        {
            this.services = services;
        }
        public Builder BeforeEveryRequest<T>()
            where T : class,IHandler
        {
            AddToServices<T>();
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.BeforeEveryRequest, new HandlerExecuter<T>(services), AlwaysAction));
            return this;
        }
        public Builder AfterEveryRequest<T>()
            where T : class, IHandler
        {
            AddToServices<T>();
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.AfterEveryRequest, new HandlerExecuter<T>(services), AlwaysAction));
            return this;
        }
        public Builder OnError<T>()
            where T : class, IHandler
        {
            AddToServices<T>();
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnError, new HandlerExecuter<T>(services), AlwaysAction));
            return this;
        }
        public Builder OnNotHandled<T>()
            where T : class, IHandler
        {
            AddToServices<T>();
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnNotHandled, new HandlerExecuter<T>(services), AlwaysAction));
            return this;
        }
        public Builder ValidateRequest<T>(RequestMethod method, string pathSelector)
            where T : class, IHandler
        {
            AddToServices<T>();
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.ValidateRequest, new HandlerExecuter<T>(services), MakeSelectorAction(method, pathSelector),MakePathParams(pathSelector)));
            return this;
        }
        public Builder OnRequest<T>(RequestMethod method, string pathSelector)
            where T : class, IHandler
        {
            AddToServices<T>();
            handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnRequest, new HandlerExecuter<T>(services), MakeSelectorAction(method, pathSelector), MakePathParams(pathSelector)));
            return this;
        }
        public void Dispose()
        {
            IRequestResolver resolver = new RequestResolver(handlerWrappers.ToArray());
            services.AddSingleton(resolver);
        }
        private void AddToServices<T>() where T : class, IHandler
        {
            if (services.Contains(new ServiceDescriptor(typeof(T), typeof(T), ServiceLifetime.Singleton))) return;
            services.AddSingleton<T,T>();
        }
        private static Dictionary<string, int> MakePathParams(string pathSelector)
        {
            var rtn = new Dictionary<string, int>();
            int count = 0;
            foreach (var i in pathSelector.Split("//"))
            {
                if (i.StartsWith("{") && i.EndsWith("}"))
                {
                    var key = i.Replace("{", string.Empty).Replace("}", string.Empty);
                    rtn[key] = count;
                }
                count++;
            }
            return rtn;
        }
        private Func<RequestContext, ResponseContent, bool> MakeSelectorAction(RequestMethod method, string pathSelector)
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
}
