using System;
using System.Collections.Generic;
using System.Linq;

namespace Officium.Tools.Request
{
    public class RequestContext
    {
        private Dictionary<string, string> internalParams = new Dictionary<string, string>();
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
            if (TryGetValue(internalParams, key, ref rtn)) return rtn;
            else if (TryGetPathValue(PathParams, Path, key, ref rtn)) return rtn;
            else if (TryGetValue(QueryParams, key, ref rtn)) return rtn;
            else if (TryGetValue(BodyParams, key, ref rtn)) return rtn;
            return rtn;
        }

        private static bool TryGetValue(Dictionary<string, string> paramsDict, string key, ref string rtn)
        {
            if (paramsDict == null) return false;
            if (paramsDict.Any() == false) return false;
            if (paramsDict.ContainsKey(key.ToLower()) == false) return false;
            rtn = paramsDict[key.ToLower()];
            return true;
        }

        public void SetValue(string key, string value)
        {
            internalParams[key.ToLower()] = value; 
        }

        private static bool TryGetPathValue(Dictionary<string, int> pathParams, string path, string key, ref string rtn)
        {
            if (pathParams == null) return false;
            if (pathParams.Any() == false) return false;
            if (pathParams.ContainsKey(key) == false) return false;
            var index = pathParams[key];
            var pathParts = path.Split("/");
            if (pathParts.Length < index) return false;
            rtn = pathParts[index];
            return true;
        }
    }
}
