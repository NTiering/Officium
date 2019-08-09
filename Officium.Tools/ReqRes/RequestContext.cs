using Officium.Tools.Handlers;

namespace Officium.Tools.ReqRes
{
    public class RequestContext
    {
        public Method RequestMethod { get; set; }
        public string Path { get; set; }
    }
}
