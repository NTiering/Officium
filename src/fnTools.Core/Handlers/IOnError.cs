using Officium.Core.ReqRes;
using System;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IOnError 
    {
        Task Handle(RequestContext requestContext, ResponseContent responseContent, Exception exception);
    }
}
