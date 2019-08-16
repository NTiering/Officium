namespace Officium.Tools.Helpers
{
    public static class WithDefaultExt
    {
        public static string WithDefault(this string s, string defaultValue)
        {
            var rtn = s.IsNullOrWhitespace() ? defaultValue : s;
            return rtn;
        }
    }
}
