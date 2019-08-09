using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IValidationHandlerFunction 
    {
        Method Method { get; }
        string PathSelector { get; set; }
        void Handle(RequestContext requestContext, ResponseContent response);
    }
}
