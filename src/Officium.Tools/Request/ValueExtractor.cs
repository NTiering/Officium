using System.Collections.Generic;
using System.Linq;

namespace Officium.Tools.Request
{
    public class ValueExtractor : IValueExtractor
    {
        public bool TryGetPathValue(Dictionary<string, int> pathParams, string path, string key, ref string rtn)
        {
            if (pathParams == null) return false;
            if (pathParams.Any() == false) return false;
            var dictKey = pathParams.Keys.FirstOrDefault(x => string.Compare(x, key, true) == 0);
            if (dictKey == null) return false;         
            var index = pathParams[dictKey];
            var pathParts = path.Split("/");
            if (pathParts.Length < index) return false;
            rtn = pathParts[index];
            return true;
        }

        public bool TryGetValue(Dictionary<string, string> paramsDict, string key, ref string rtn)
        {
            if (paramsDict == null) return false;
            if (paramsDict.Any() == false) return false;
            var dictKey = paramsDict.Keys.FirstOrDefault(x => string.Compare(x, key, true) == 0);
            if (dictKey == null) return false;
            rtn = paramsDict[dictKey];
            return true;
        }
    }
}
