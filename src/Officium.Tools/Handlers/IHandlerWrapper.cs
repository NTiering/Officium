namespace Officium.Tools.Handlers
{
    using Officium.Tools.Request;
    using Officium.Tools.Response;

    public interface IHandlerWrapper 
    {
        HandlerOrder Order { get; }
        bool CanHandleRequest(IRequestContext request, IResponseContent response);
        void HandleRequest(IRequestContext request, IResponseContent response);        
    }
}
