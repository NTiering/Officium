using System;
using System.Collections.Generic;
using System.Text;

namespace fnTools.Core.ExtMethods
{
    internal static class StringExtMethods
    {
        public static string RemoveTrailingAndLeadingSlashes(this string s)
        {
            var rtn = s.Trim(new[] { '/', '\\' });
            return rtn;
        }
    }
}
