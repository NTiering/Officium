using fnTools.Core.HandlerCollection;
using fnTools.Core.Handlers;
using fnTools.Core.Startup;
using Microsoft.Extensions.DependencyInjection;
using Officium.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Officium.Core.Startup
{
    public class HandlerRegisterHelper
    {
        private readonly Type handlerType;
        private readonly IServiceCollection services;

        public HandlerRegisterHelper(Type t, IServiceCollection services)
        {
            this.handlerType = typeof(t);
            this.services = services;
        }

        public bool TryAdd(Type t)
        {
            if (handlerType.IsAssignableFrom(t))
            {
                services.AddSingleton(handlerType, t);
                return true;
            }
            return false;
        }
    }

    public class Builder
    {
        private readonly IServiceCollection serviceCollection;
        private readonly List<HandlerRegisterHelper> handlerRegisters = new List<HandlerRegisterHelper>();

        public Builder(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
            handlerRegisters.AddRange(new[]
                {
                new HandlerRegisterHelper(typeof(IAfterEveryRequestHandler),this.serviceCollection),
                new HandlerRegisterHelper(typeof(IBeforeEveryRequestHandler),this.serviceCollection),
                new HandlerRegisterHelper(typeof(IOnErrorHandler),this.serviceCollection),
                new HandlerRegisterHelper(typeof(IOnNotHandledHandler),this.serviceCollection),

                new HandlerRegisterHelper(typeof(IRequestHandlerWrapper),this.serviceCollection),
                new HandlerRegisterHelper(typeof(IValidationHandlerWrapper),this.serviceCollection),
                }
            );
        }

        public Builder Add<T>()
        {
            var added = handlerRegisters.Any(x => x.TryAdd(typeof(T)));
            if (added == false)
            {
                throw new InvalidOperationException("Unable to add type of " + typeof(T).FullName);
            }
            return this;
        }

        
        //public Builder Add(IOnErrorHandler req)
        //{
        //    functionHandler.Add(req);
        //    return this;
        //}
        //public Builder Add(Method method, string pathSelector, IRequestHandlerFunction handler)
        //{
        //    functionHandler.Add(method, pathSelector, handler);
        //    return this;
        //}
        ////public Builder Add(Method method, string pathSelector, IValidationHandler handler)
        ////{
        ////    functionHandler.Add(method, pathSelector, handler);
        ////    return this;
        ////}
        //public IFunctionHandler GetHandler()
        //{
        //    return functionHandler;
        //}


    }
}
