using System.Collections.Generic;
using System.Linq;
using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
public class ValidationFunctionHandlerCollection : IValidationFunctionHandler
    {
        private readonly List<IValidationHandlerWrapper> handlers = new List<IValidationHandlerWrapper>();

        public ValidationFunctionHandlerCollection(IValidationHandlerWrapper[] validationHandlerWrappers)
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
}