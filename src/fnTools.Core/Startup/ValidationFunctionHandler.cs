using System.Collections.Generic;
using System.Linq;
using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.Startup
{
    public class ValidationFunctionHandler : IValidationFunctionHandler
    {
        private readonly List<IValidationHandlerWrapper> handlers = new List<IValidationHandlerWrapper>();

        public ValidationFunctionHandler(IValidationHandlerWrapper[] validationHandlerWrappers)
        {
            handlers = new List<IValidationHandlerWrapper>(validationHandlerWrappers);
        }

        public void Handle(RequestContext request, ResponseContent response)
        {
            handlers
                .Where(x => CanHandle(x, request))
                .ToList()
                .ForEach(handler => handler.Handle(request, response));
        }

        private bool CanHandle(IValidationHandlerWrapper handlerWrapper, RequestContext request)
        {
            return true;
        }
    }

    public class ValidationHandlerWrapper : IValidationHandlerWrapper
    {
        public ValidationHandlerWrapper(Method method, string pathSelector, IValidationHandler handler)
        {
            Method = method;
            PathSelector = pathSelector;
            Handler = handler;
        }

        public Method Method { get; private set; }
        public string PathSelector { get; private set; }
        public IValidationHandler Handler { get; private set; }
        public void Handle(RequestContext request, ResponseContent response) => Handler?.Handle(request, response);
    }
}
