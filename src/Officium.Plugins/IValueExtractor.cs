namespace Officium.Plugins
{
    using System.Collections.Generic;
    public interface IValueExtractor
    {
        bool TryGetPathValue(Dictionary<string, int> pathParams, string path, string key, ref string rtn);
        bool TryGetValue(Dictionary<string, string> paramsDict, string key, ref string rtn);
    }
}