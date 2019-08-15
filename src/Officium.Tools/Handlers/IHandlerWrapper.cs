namespace Officium.Tools.Handlers
{
    using Officium.Tools.Request;
    using Officium.Tools.Response;

    public interface IHandlerWrapper 
    {
        HandlerOrder Order { get; }
        bool CanHandleRequest(RequestContext request, ResponseContent response);
        void HandleRequest(RequestContext request, ResponseContent response);
    }
}
