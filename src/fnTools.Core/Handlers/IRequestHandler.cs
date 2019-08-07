using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IRequestHandler 
    {
        void Handle(RequestContext requestContext, ResponseContent responseContent);
    }
}
