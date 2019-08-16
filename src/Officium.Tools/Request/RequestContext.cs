namespace Officium.Tools.Request
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    public class RequestContext : IRequestContext
    {
        private readonly Dictionary<string, string> internalParams = new Dictionary<string, string>();
        internal Dictionary<string, string> BodyParams { get; set; }
        internal Dictionary<string, string> QueryParams { get; set; }
        internal RequestMethod RequestMethod { get; set; }
        internal string Path { get; set; }
        internal Dictionary<string, int> PathParams { get; set; }
        internal Dictionary<string, string> HeadersParams { get; set; }
        public ClaimsIdentity Identity { get; set; }
        public readonly Guid Id = Guid.NewGuid();
        private readonly IValueExtractor valueExtractor;

        internal RequestContext(IValueExtractor valueExtractor)
        {
            this.valueExtractor = valueExtractor;
        }

        public string GetHeaderValue(string key)
        {
            var rtn = string.Empty;
            if (TryGetValue(HeadersParams, key, ref rtn)) return rtn;
            return string.Empty;
        }

        public string GetValue(string key)
        {
            var rtn = string.Empty;          
            if (TryGetPathValue(PathParams, Path, key, ref rtn)) return rtn;
            else if (TryGetValue(QueryParams, key, ref rtn)) return rtn;
            else if (TryGetValue(BodyParams, key, ref rtn)) return rtn;
            return rtn;
        }

        private bool TryGetValue(Dictionary<string, string> paramsDict, string key, ref string rtn)
        {
            var result = valueExtractor.TryGetValue(paramsDict, key, ref rtn);
            return result;
        }

        public void SetInternalValue(string key, string value)
        {
            internalParams[key.ToLower()] = value;

        }

        public string GetInternalValue(string key)
        {
            string value = null;
            var rtn = TryGetValue(internalParams, key, ref value);
            return rtn ? value : string.Empty;
        }
         

        private bool TryGetPathValue(Dictionary<string, int> pathParams, string path, string key, ref string rtn)
        {
            var result = valueExtractor.TryGetPathValue(pathParams, path, key, ref rtn);
            return result;
        }
    }
}
