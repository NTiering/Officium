using Officium.Core.ReqRes;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IValidationHandler : IBaseHandler
    {
        void Handle(RequestContext requestContext, ResponseContent response);
    }
}
