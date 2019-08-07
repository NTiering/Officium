using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.Startup
{
    //
    public class OnErrorHandler
    {
        private readonly List<IOnError> handlers = new List<IOnError>();

        public void Add(IOnError handler)
        {
            handlers.Add(handler);
        }

        public void Handle(RequestContext request, ResponseContent response, Exception ex)
        {
            handlers.ForEach(x => x.Handle(request, response, ex));
        }
    }

    public class AfterFunctionHandler
    {
        private readonly List<IAfterEveryRequest> handlers = new List<IAfterEveryRequest>();

        public void Add(IAfterEveryRequest handler)
        {
            handlers.Add(handler);
        }

        public void Handle(RequestContext request, ResponseContent response)
        {
            handlers.ForEach(handler => handler.Handle(request, response));
        }
    }
    public class BeforeFunctionHandler
    {
        private readonly List<IBeforeEveryRequest> handlers = new List<IBeforeEveryRequest>();

        public void Add(IBeforeEveryRequest handler)
        {
            handlers.Add(handler);
        }

        public void Handle(RequestContext request, ResponseContent response)
        {
            handlers.ForEach(handler => handler.Handle(request, response));
        }
    }

    public class RequestFunctionHandler
    {
        private readonly List<RequestHandlerWrapper> handlers = new List<RequestHandlerWrapper>();

        public void Add(Method method, string pathSelector, IRequestHandler handler)
        {
            handlers.Add(new RequestHandlerWrapper { Method = method, PathSelector = pathSelector, Handler = handler });
        }

        public void Handle(RequestContext request, ResponseContent response)
        {
            handlers
                .Where(x=> CanHandle(x,request))
                .ToList()
                .ForEach(handler => handler.Handle(request, response));
        }

        private bool CanHandle(RequestHandlerWrapper handlerWrapper, RequestContext request)
        {
            return true;
        }

        private class RequestHandlerWrapper
        {
            public Method Method { get; set; }
            public string PathSelector { get; set; }
            public IRequestHandler Handler { get; set; }
            public void Handle(RequestContext request, ResponseContent response) => Handler?.Handle(request, response);
        }
    }

    public class ValidationFunctionHandler
    {
        private readonly List<ValidationHandlerWrapper> handlers = new List<ValidationHandlerWrapper>();

        public void Add(Method method, string pathSelector, IValidationHandler handler)
        {
            handlers.Add(new ValidationHandlerWrapper { Method = method, PathSelector = pathSelector, Handler = handler });
        }

        public void Handle(RequestContext request, ResponseContent response)
        {
            handlers
                .Where(x => CanHandle(x, request))
                .ToList()
                .ForEach(handler => handler.Handle(request, response));
        }

        private bool CanHandle(ValidationHandlerWrapper handlerWrapper, RequestContext request)
        {
            return true;
        }

        private class ValidationHandlerWrapper
        {
            public Method Method { get; set; }
            public string PathSelector { get; set; }
            public IValidationHandler Handler { get; set; }
            public void Handle(RequestContext request, ResponseContent response) => Handler?.Handle(request, response);
        }
    }

    public class FunctionHandler
    {
        private readonly BeforeFunctionHandler beforeFunctions = new BeforeFunctionHandler();
        private readonly AfterFunctionHandler afterFunctions = new AfterFunctionHandler();
        private readonly OnErrorHandler onErrorFunctions = new OnErrorHandler();
        private readonly RequestFunctionHandler requestFunctions = new RequestFunctionHandler();
        private readonly ValidationFunctionHandler validationFunctions = new ValidationFunctionHandler();
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            try
            {
                beforeFunctions.Handle(request, response);
                validationFunctions.Handle(request, response);
                requestFunctions.Handle(request, response);
                afterFunctions.Handle(request, response);
            }
            catch (Exception ex)
            {
                onErrorFunctions.Handle(request, response, ex);
            }            
        }

        public void Add(IBeforeEveryRequest req) 
            => beforeFunctions.Add(req);
        public void Add(IAfterEveryRequest req) 
            => afterFunctions.Add(req);
        public void Add(IOnError req) 
            => onErrorFunctions.Add(req);
        public void Add(Method method, string pathSelector, IRequestHandler handler)
            => requestFunctions.Add(method, pathSelector, handler);
        public void Add(Method method, string pathSelector, IValidationHandler handler)
            => validationFunctions.Add(method, pathSelector, handler);
    }
}
