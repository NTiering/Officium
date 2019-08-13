using Officium.Tools.ReqRes;
using System;
using System.Linq;

namespace Officium.Tools.Handlers
{
    public class RequestResolver : IRequestResolver
    {
        private readonly IHandlerWrapper[] handlers;

        public RequestResolver(IHandlerWrapper[] handlers)
        {
            this.handlers = handlers;
        }

        public ResponseContent Execute(RequestContext req)
        {
            var res = new ResponseContent { StatusCode = 200 };
            try
            {
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

        private int ExecuteHandlers(RequestContext req, ResponseContent res, HandlerOrder handlerOrder)
        {
            var chosen = handlers
                .Where(x => x.Order == handlerOrder)
                .Where(x => x.CanHandleRequest(req, res))
                .ToList();

            chosen.ForEach(x => x.HandleRequest(req, res));

            return chosen.Count();
        }
    }
}
