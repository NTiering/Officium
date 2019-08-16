namespace Officium.Tools.Request
{
    public interface IRequestContext
    {
        string GetValue(string key);
        void SetInternalValue(string key, string value);
        string GetInternalValue(string key);
        string GetHeaderValue(string key);
    }
}