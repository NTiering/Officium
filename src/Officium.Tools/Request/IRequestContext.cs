namespace Officium.Tools.Request
{
    public interface IRequestContext
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}