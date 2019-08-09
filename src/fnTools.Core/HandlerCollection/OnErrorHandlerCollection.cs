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
    public class OnErrorHandlerCollection : IOnErrorHandlerCollection
    {
        private readonly List<IOnErrorHandler> handlers = new List<IOnErrorHandler>();

        public OnErrorHandlerCollection(IOnErrorHandler[] handlers)
        {
            this.handlers.AddRange(handlers);
        }

        public void Handle(RequestContext request, ResponseContent response, Exception ex)
        {
            handlers.ForEach(x => x.Handle(request, response, ex));
        }
    }
}