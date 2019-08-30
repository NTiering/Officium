namespace Officium.Tools.Request
{
    using Officium.Tools.Handlers;
    using Officium.Tools.Response;
    using System;
    using System.Linq;
    public class RequestResolver : IRequestResolver
    {
        private readonly IHandlerWrapper[] _handlers;

        public RequestResolver(IHandlerWrapper[] handlers)
        {
            _handlers = handlers;
        }

        public IResponseContent Execute(IRequestContext req)
        {
            var res = new ResponseContent { StatusCode = 200 };
            try
            {
                ExecuteHandlers(req, res, HandlerOrder.Authorise);
                ExecuteHandlers(req, res, HandlerOrder.BeforeEveryRequest);
                ExecuteHandlers(req, res, HandlerOrder.ValidateRequest);
                var reqCount = ExecuteHandlers(req, res, HandlerOrder.OnRequest);
                if (reqCount == 0)
                {
                    ExecuteHandlers(req, res, HandlerOrder.OnNotHandled);
                }
                ExecuteHandlers(req, res, HandlerOrder.AfterEveryRequest);
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                ExecuteHandlers(req, res, HandlerOrder.OnError);
            }

            return res;
        }

        private int ExecuteHandlers(IRequestContext req, ResponseContent res, HandlerOrder handlerOrder)
        {
            var chosen = _handlers
                .Where(x => x.Order == handlerOrder)
                .Where(x => x.CanHandleRequest(req, res))
                .ToList();

            chosen.ForEach(x => x.HandleRequest(req, res));
            return chosen.Count();
        }
    }
}
