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
        private readonly Func<IRequestContext, IResponseContent, bool> _canHandleAction;
        private readonly Dictionary<string, int> _pathParams;

        public HandlerWrapper(HandlerOrder order, IHandler handler, Func<IRequestContext, IResponseContent, bool> canHandleAction)
            : this(order, handler, canHandleAction, new Dictionary<string, int>())
        {
        }

        public HandlerWrapper(HandlerOrder order, IHandler handler, Func<IRequestContext, IResponseContent, bool> canHandleAction, Dictionary<string, int> pathParams)
        {
            Order = order;
            _handler = handler;
            _canHandleAction = canHandleAction;
            _pathParams = pathParams;
        }

        public bool CanHandleRequest(IRequestContext request, IResponseContent response)
        {
            request.PathParams = _pathParams;
            return _canHandleAction(request, response);
        }

        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            request.PathParams = _pathParams;
            _handler.HandleRequest(request, response);
        }

        
    }
}
