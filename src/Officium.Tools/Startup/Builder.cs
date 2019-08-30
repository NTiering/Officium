namespace Officium.Tools.Handlers
{
    using Microsoft.Extensions.DependencyInjection;
    using Officium.Tools.Helpers;
    using Officium.Tools.Request;
    using Officium.Tools.Response;
    using System;
    using System.Collections.Generic;

    public class Builder : IDisposable
    {
        private readonly List<IHandlerWrapper> _handlerWrappers = new List<IHandlerWrapper>();
        private readonly IServiceCollection _services;
        private static readonly IRouteMatcher _routeMatcher = new RouteMatcher();
        private static readonly IPathParamExtractor _pathParamExtractor = new PathParamExtractor();

        public Builder(IServiceCollection services)
        {
            _services = services;            
        }
        
        public Builder BeforeEveryRequest<T>()
            where T : class, IHandler
        {
            AddToServices<T>();
            _handlerWrappers.Add(new HandlerWrapper(HandlerOrder.BeforeEveryRequest, new HandlerExecuter<T>(_services), AlwaysAction));
            return this;
        }
        public Builder Authorise<T>()
            where T : class, IHandler
        {
            AddToServices<T>();
            _handlerWrappers.Add(new HandlerWrapper(HandlerOrder.Authorise, new HandlerExecuter<T>(_services), AlwaysAction));

            return this;
        }

        public Builder AfterEveryRequest<T>()
            where T : class, IHandler
        {
            AddToServices<T>();
            _handlerWrappers.Add(new HandlerWrapper(HandlerOrder.AfterEveryRequest, new HandlerExecuter<T>(_services), AlwaysAction));
            return this;
        }

        public Builder OnError<T>()
            where T : class, IHandler
        {
            AddToServices<T>();
            _handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnError, new HandlerExecuter<T>(_services), AlwaysAction));
            return this;
        }

        public Builder OnNotHandled<T>()
            where T : class, IHandler
        {
            AddToServices<T>();
            _handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnNotHandled, new HandlerExecuter<T>(_services), AlwaysAction));
            return this;
        }

        public Builder ValidateRequest<T>(RequestMethod method, string pathSelector)
            where T : class, IHandler
        {
            AddToServices<T>();
            _handlerWrappers.Add(new HandlerWrapper(HandlerOrder.ValidateRequest, new HandlerExecuter<T>(_services), MakeSelectorAction(method, pathSelector),MakePathParams(pathSelector)));
            return this;
        }

        public Builder OnRequest<T>(RequestMethod method, string pathSelector)
            where T : class, IHandler
        {
            AddToServices<T>();
            _handlerWrappers.Add(new HandlerWrapper(HandlerOrder.OnRequest, new HandlerExecuter<T>(_services), MakeSelectorAction(method, pathSelector), MakePathParams(pathSelector)));
            return this;
        }
        public void Dispose()
        {
            IRequestResolver resolver = new RequestResolver(_handlerWrappers.ToArray());
            _services.AddSingleton(resolver);
        }
        private void AddToServices<T>() where T : class, IHandler
        {
            if (_services.Contains(new ServiceDescriptor(typeof(T), typeof(T), ServiceLifetime.Singleton))) return;
            _services.AddSingleton<T,T>();
        }
        private static Dictionary<string, int> MakePathParams(string pathSelector)
        {
            var rtn = _pathParamExtractor.MakePathParams(pathSelector);
            return rtn;            
        }
        private Func<IRequestContext, IResponseContent, bool> MakeSelectorAction(RequestMethod method, string pathSelector)
        {
            return (req, res) =>
            {
                var rtn =
                    req.RequestMethod == method &&
                    _routeMatcher.Matches(pathSelector, req.Path);
                return rtn;
            };
        }
        private static Func<IRequestContext, IResponseContent, bool> AlwaysAction
        {
            get { return (req, res) => { return true; }; }
        }
    }
}
