using fnTools.Core.Handlers;
using fnTools.Core.Startup;
using Microsoft.Extensions.DependencyInjection;
using Officium.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Core.Startup
{
    public class Builder
    {
        private readonly IFunctionHandler functionHandler;

        public Builder(IFunctionHandler functionHandler, IServiceCollection ser)
        {
            this.functionHandler = functionHandler;
        }

        public Builder Add(IBeforeEveryRequest req)
        {
            functionHandler.Add(req);
            return this;
        }

        public Builder Add(IAfterEveryRequest req)
        {
            functionHandler.Add(req);
            return this;
        }

        public Builder Add(IOnError req)
        {
            functionHandler.Add(req);
            return this;
        }
        public Builder Add(Method method, string pathSelector, IRequestHandler handler)
        {
            functionHandler.Add(method, pathSelector, handler);
            return this;
        }
        //public Builder Add(Method method, string pathSelector, IValidationHandler handler)
        //{
        //    functionHandler.Add(method, pathSelector, handler);
        //    return this;
        //}
        public IFunctionHandler GetHandler()
        {
            return functionHandler;
        }

        
    }
}
