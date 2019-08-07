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

    public interface IBeforeEveryRequest : IBaseHandler
    {
    }

    public interface IAfterEveryRequest : IBaseHandler
    {
    }

    public interface IOnError : IBaseHandler
    {
    }

    public interface IOnNotHandled : IBaseHandler
    {
    }

    public interface IRequestHandler : IBaseHandler
    {
    }

    public interface IValidationHandler : IBaseHandler
    {
    }
}
