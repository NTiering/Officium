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
    public class RequestHandlerWrapper : IRequestHandlerWrapper
    {
        public Method Method { get; set; }
        public string PathSelector { get; set; }
        public IRequestHandlerFunction Handler { get; set; }
        public void Handle(RequestContext request, ResponseContent response) => Handler?.Handle(request, response);
    }
}
