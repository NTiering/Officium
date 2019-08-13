using Officium.Tools.Request;
using Officium.Tools.Response;
using System;
using System.Collections.Generic;

namespace Officium.Tools.Handlers
{
    internal class HandlerWrapper : IHandlerWrapper
    {
        public HandlerOrder Order { get; private set; }

        private readonly IHandler handler;
        private readonly Func<RequestContext, ResponseContent, bool> canHandleAction;
        private readonly Dictionary<string, int> pathParams;

        public HandlerWrapper(HandlerOrder order, IHandler handler, Func<RequestContext, ResponseContent, bool> canHandleAction)
            : this(order, handler, canHandleAction, new Dictionary<string, int>())
        {
        }

        public HandlerWrapper(HandlerOrder order, IHandler handler, Func<RequestContext, ResponseContent, bool> canHandleAction, Dictionary<string, int> pathParams)
        {
            this.Order = order;
            this.handler = handler;
            this.canHandleAction = canHandleAction;
            this.pathParams = pathParams;
        }

        public bool CanHandleRequest(RequestContext request, ResponseContent response)
        {
            request.PathParams = pathParams;
            return canHandleAction(request, response);
        }
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            request.PathParams = pathParams;
            handler.HandleRequest(request, response);
        }
    }
}
