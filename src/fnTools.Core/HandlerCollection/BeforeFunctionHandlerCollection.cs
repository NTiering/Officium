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
    public class BeforeFunctionHandlerCollection : IBeforeFunctionHandlerCollection
    {
        private readonly List<IBeforeEveryRequestHandler> handlers = new List<IBeforeEveryRequestHandler>();

        public BeforeFunctionHandlerCollection(IBeforeEveryRequestHandler[] handlers)
        {
            this.handlers.AddRange(handlers);
        }

        public void Handle(RequestContext request, ResponseContent response)
        {
            handlers.ForEach(handler => handler.Handle(request, response));
        }
    }
}