﻿using Officium.Tools.ReqRes;
using System.Text;

namespace Officium.Tools.Handlers
{
    public interface IHandler
    {
        void HandleRequest(RequestContext request, ResponseContent response);
    }
}
