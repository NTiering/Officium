using System;
using Officium.Core.ReqRes;

namespace fnTools.Core.HandlerCollection
{
    public interface IOnErrorHandlerCollection
    {
        void Handle(RequestContext request, ResponseContent response, Exception ex);
    }
}