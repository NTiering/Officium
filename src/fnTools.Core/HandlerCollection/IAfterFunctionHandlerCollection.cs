using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public interface IAfterFunctionHandlerCollection
    {
        void Handle(RequestContext request, ResponseContent response);
    }
}