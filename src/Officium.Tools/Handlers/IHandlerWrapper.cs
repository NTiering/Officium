using Officium.Tools.Request;
using Officium.Tools.Response;

namespace Officium.Tools.Handlers
{
    public interface IHandlerWrapper 
    {
        HandlerOrder Order { get; }
        bool CanHandleRequest(RequestContext request, ResponseContent response);
        void HandleRequest(RequestContext request, ResponseContent response);


    }
}
