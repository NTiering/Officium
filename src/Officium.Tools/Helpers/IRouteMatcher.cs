namespace Officium.Tools.Helpers
{
    public interface IRouteMatcher
    {
        bool Matches(string source, string candidate);
    }
}