using Officium.Tools.ReqRes;

namespace Officium.Tools.Handlers
{
    public interface IHandlerWrapper 
    {
        HandlerOrder Order { get; }
        bool CanHandleRequest(RequestContext request, ResponseContent response);
        void HandleRequest(RequestContext request, ResponseContent response);


    }
}
