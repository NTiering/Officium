using System.Collections.Generic;
using System.Linq;
using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public class ValidationHandlerWrapper : IValidationHandlerWrapper
    {
        public ValidationHandlerWrapper(Method method, string pathSelector, T handler)
        {
            Method = method;
            PathSelector = pathSelector;
            Handler = handler;
        }

        public Method Method { get; private set; }
        public string PathSelector { get; private set; }
        public IValidationHandlerFunction Handler { get; private set; }
        public void Handle(RequestContext request, ResponseContent response) => Handler?.Handle(request, response);
    }
}