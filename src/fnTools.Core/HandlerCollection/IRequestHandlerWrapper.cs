using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public interface IRequestHandlerWrapper
    {
        IRequestHandlerFunction Handler { get; set; }
        Method Method { get; set; }
        string PathSelector { get; set; }

        void Handle(RequestContext request, ResponseContent response);
    }
}