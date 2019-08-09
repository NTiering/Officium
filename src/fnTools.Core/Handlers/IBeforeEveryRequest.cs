using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IBeforeEveryRequestHandler 
    {
        Task Handle(RequestContext requestContext, ResponseContent responseContent);
    }
}
