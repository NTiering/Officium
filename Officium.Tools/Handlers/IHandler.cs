using Officium.Tools.Request;
using Officium.Tools.Response;

namespace Officium.Tools.Handlers
{
    public interface IHandler
    {
        void HandleRequest(RequestContext request, ResponseContent response);
    }
}
