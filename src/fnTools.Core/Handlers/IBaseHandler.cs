using Officium.Core.ReqRes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Officium.Core.Handlers
{
    public interface IBaseHandler
    {
        Task Handle(RequestContext requestContext, ResponseContent responseContent);
    }
}
