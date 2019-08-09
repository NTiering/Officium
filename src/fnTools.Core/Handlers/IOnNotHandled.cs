using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IOnNotHandledHandler 
    {
        Task Handle(RequestContext requestContext, ResponseContent responseContent);
    }
}
