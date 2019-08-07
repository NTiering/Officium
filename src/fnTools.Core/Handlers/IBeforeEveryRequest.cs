using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IBeforeEveryRequest 
    {
        Task Handle(RequestContext requestContext, ResponseContent responseContent);
    }
}
