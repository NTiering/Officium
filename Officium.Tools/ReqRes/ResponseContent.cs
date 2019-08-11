using System.Collections.Generic;

namespace Officium.Tools.ReqRes
{
    public class ResponseContent
    {
        internal ResponseContent()
        {

        }
        public readonly List<ValidationError> ValidationError = new List<ValidationError>();
        public int StatusCode { get; set; }
        public dynamic Result { get; set; }
    }
}
