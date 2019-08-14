using System;
using System.Collections.Generic;
using System.Linq;

namespace Officium.Tools.Request
{
    public class RequestContext
    {
        internal Dictionary<string, string> BodyParams { get; set; }
        internal Dictionary<string, string> QueryParams { get; set; }
        internal RequestMethod RequestMethod { get; set; }
        internal string Path { get; set; }
        internal Dictionary<string, int> PathParams { get; set; }

        public readonly Guid Id = Guid.NewGuid(); 

        internal RequestContext()
        {                
        }


        public string GetValue(string key)
        {
            var rtn = string.Empty;
            if (TryGetPathValue(PathParams, Path, key, ref rtn)) return rtn;
            else if (TryGetQueryValue(QueryParams, key, ref rtn)) return rtn;
            else if (TryGetBodyValue(BodyParams, key, ref rtn)) return rtn;
            return rtn;
        }

        private static bool TryGetBodyValue(Dictionary<string, string> bodyParams, string key, ref string rtn)
        {
            if (bodyParams == null) return false;
            if (bodyParams.Any() == false) return false;
            if (bodyParams.ContainsKey(key) == false) return false;
            rtn = bodyParams[key];
            return true;
        }

        private static bool TryGetQueryValue(Dictionary<string, string> queryParams, string key, ref string rtn)
        {
            if (queryParams == null) return false;
            if (queryParams.Any() == false) return false;
            if (queryParams.ContainsKey(key) == false) return false;
            rtn = queryParams[key];
            return true;
        }

        private static bool TryGetPathValue(Dictionary<string, int> pathParams, string path, string key, ref string rtn)
        {
            if (pathParams == null) return false;
            if (pathParams.Any() == false) return false;
            if (pathParams.ContainsKey(key) == false) return false;
            var index = pathParams[key];
            var pathParts = path.Split("//");
            if (pathParts.Length < index) return false;
            rtn = pathParts[index];
            return true;
        }
    }
}
