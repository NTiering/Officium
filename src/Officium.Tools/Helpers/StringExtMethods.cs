using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Officium.Tools.Helpers
{
    internal static class StringExtMethods
    {
        public static string RemoveTrailingAndLeadingSlashes(this string s)
        {
            var rtn = s.Trim(new[] { '/', '\\' });
            return rtn;
        }

        public static string[] SplittIntoParts(this string s)
        {
            var rtn = s.Split("/").Where(x => string.IsNullOrWhiteSpace(x) == false).ToArray();
            return rtn;
        }

        public static bool IsNullOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        public static bool IsNotNullOrWhitespace(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }
    }
}
