using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.Startup
{
    public interface IFunctionHandler
    {
        void Add(IAfterEveryRequest req);
        void Add(IBeforeEveryRequest req);
        void Add(IOnError req);
        void Add(Method method, string pathSelector, IRequestHandler handler);
        void HandleRequest(RequestContext request, ResponseContent response);
    }
}