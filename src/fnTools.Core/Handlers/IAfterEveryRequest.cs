using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IAfterEveryRequest : IBaseHandler
    {
        Task Handle(RequestContext requestContext, ResponseContent responseContent);
    }
}
