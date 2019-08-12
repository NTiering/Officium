using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Officium.Tools.Handlers;

namespace Officium.Tools.ReqRes
{
    public class RequestContext
    {
        internal RequestContext()
        {
                
        }

        internal Dictionary<string, string> BodyParams { get; set; }
        internal Dictionary<string, StringValues> QueryParams { get; set; }
        internal RequestMethod RequestMethod { get; set; }
        internal string Path { get; set; }
        internal Exception Exception { get; set; }
        internal dynamic Result { get; set; }
        internal Dictionary<string, int> PathParams { get; set; }
    }
}
