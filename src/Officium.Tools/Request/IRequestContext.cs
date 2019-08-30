using System.Collections.Generic;

namespace Officium.Tools.Request
{
    public interface IRequestContext
    {
        string GetValue(string key);
        void SetInternalValue(string key, string value);
        string GetInternalValue(string key);
        string GetHeaderValue(string key);
        Dictionary<string, int> PathParams { get; set; }
        RequestMethod RequestMethod { get; set; }
        string Path { get; set; }
    }
}