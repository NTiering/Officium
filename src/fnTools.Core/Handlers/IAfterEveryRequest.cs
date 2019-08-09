using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IAfterEveryRequestHandler 
    {
        Task Handle(RequestContext requestContext, ResponseContent responseContent);
    }
}
