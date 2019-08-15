namespace Officium.Tools.Handlers
{
    using Officium.Tools.Request;
    using Officium.Tools.Response;

    public interface IHandler
    {
        void HandleRequest(RequestContext request, ResponseContent response);
    }
}
