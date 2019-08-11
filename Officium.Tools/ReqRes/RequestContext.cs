using System;
using Officium.Tools.Handlers;

namespace Officium.Tools.ReqRes
{
    public class RequestContext
    {
        internal RequestContext()
        {
                
        }
        public RequestMethod RequestMethod { get; set; }
        public string Path { get; set; }
        public Exception Exception { get; set; }
        public dynamic Result { get; set; }
    }
}
