using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public interface IValidationFunctionHandler
    {
        void Handle(RequestContext request, ResponseContent response);
    }
}