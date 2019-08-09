using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public interface IBeforeFunctionHandlerCollection
    {
        void Handle(RequestContext request, ResponseContent response);
    }
}