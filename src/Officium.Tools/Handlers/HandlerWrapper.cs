namespace Officium.Tools.Handlers
{
    using Officium.Tools.Request;
    using Officium.Tools.Response;
    using System;
    using System.Collections.Generic;
    internal class HandlerWrapper : IHandlerWrapper
    {
        public HandlerOrder Order { get; private set; }
        private readonly IHandler _handler;
        private readonly Func<RequestContext, ResponseContent, bool> _canHandleAction;
        private readonly Dictionary<string, int> _pathParams;

        public HandlerWrapper(HandlerOrder order, IHandler handler, Func<RequestContext, ResponseContent, bool> canHandleAction)
            : this(order, handler, canHandleAction, new Dictionary<string, int>())
        {
        }

        public HandlerWrapper(HandlerOrder order, IHandler handler, Func<RequestContext, ResponseContent, bool> canHandleAction, Dictionary<string, int> pathParams)
        {
            Order = order;
            _handler = handler;
            _canHandleAction = canHandleAction;
            _pathParams = pathParams;
        }

        public bool CanHandleRequest(RequestContext request, ResponseContent response)
        {
            request.PathParams = _pathParams;
            return _canHandleAction(request, response);
        }

        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            request.PathParams = _pathParams;
            _handler.HandleRequest(request, response);
        }
    }
}
