using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public interface IValidationHandlerWrapper
    {
        IValidationHandlerFunction Handler { get; }
        Method Method { get; }
        string PathSelector { get; }

        void Handle(RequestContext request, ResponseContent response);
    }
}