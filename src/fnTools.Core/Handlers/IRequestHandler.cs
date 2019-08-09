using fnTools.Core.Handlers;
using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IRequestHandlerFunction 
    {
        Method Method { get; }
        string PathSelector { get; set; }
        void Handle(RequestContext requestContext, ResponseContent responseContent);
    }
}
