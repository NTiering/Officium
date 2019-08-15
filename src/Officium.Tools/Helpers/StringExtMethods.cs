namespace Officium.Tools.Helpers
{
    internal static class StringExtMethods
    {
        public static string RemoveTrailingAndLeadingSlashes(this string s)
        {
            var rtn = s.Trim(new[] { '/', '\\' });
            return rtn;
        }

        public static bool IsNullOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }      
    }
}
