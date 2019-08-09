using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public class RequestFunctionHandlerCollection : IRequestFunctionHandlerCollection
    {
        private readonly List<RequestHandlerWrapper> handlers = new List<RequestHandlerWrapper>();

        public RequestFunctionHandlerCollection(RequestHandlerWrapper[] handlers)
        {
            this.handlers.AddRange(handlers);
        }
        public void Handle(RequestContext request, ResponseContent response)
        {
            handlers
                .Where(x => CanHandle(x, request))
                .ToList()
                .ForEach(handler => handler.Handle(request, response));
        }

        private bool CanHandle(RequestHandlerWrapper handlerWrapper, RequestContext request)
        {
            return true;
        }
    }
}