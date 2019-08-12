using System;
using System.Collections.Generic;
using Officium.Tools.Handlers;

namespace Officium.Tools.ReqRes
{
    public class RequestContext
    {
        internal RequestContext()
        {
                
        }
        internal RequestMethod RequestMethod { get; set; }
        internal string Path { get; set; }
        internal Exception Exception { get; set; }
        internal dynamic Result { get; set; }
        internal Dictionary<string, int> PathParams { get; set; }
    }
}
