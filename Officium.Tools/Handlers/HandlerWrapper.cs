using Officium.Tools.ReqRes;
using System;

namespace Officium.Tools.Handlers
{
    internal class HandlerWrapper : IHandlerWrapper
    {
        private readonly IHandler handler;
        private readonly Func<RequestContext, ResponseContent, bool> canHandleAction;

        public HandlerWrapper(HandlerOrder order, IHandler handler, Func<RequestContext, ResponseContent, bool> canHandleAction)
        {
            this.Order = order;
            this.handler = handler;
            this.canHandleAction = canHandleAction;
        }

        public HandlerOrder Order { get; private set; }
        public bool CanHandleRequest(RequestContext request, ResponseContent response) => canHandleAction(request, response);
        public void HandleRequest(RequestContext request, ResponseContent response) => handler.HandleRequest(request, response);
    }
}
