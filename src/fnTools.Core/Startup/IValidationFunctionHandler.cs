using Officium.Core.ReqRes;

namespace fnTools.Core.Startup
{
    public interface IValidationFunctionHandler
    {
        void Handle(RequestContext request, ResponseContent response);
    }
}