using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public interface IRequestFunctionHandlerCollection
    {
        void Handle(RequestContext request, ResponseContent response);
    }
}