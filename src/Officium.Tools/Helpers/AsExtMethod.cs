using System;

namespace Officium.Tools.Helpers
{
    public static class AsExtMethod
    {
        public static int AsInt(this string s)
        {
            var rtn = int.TryParse(s, out var i) ? i : 0;
            return rtn;
        }

        public static DateTime AsDateTime(this string s)
        {
            var rtn = DateTime.TryParse(s, out var i) ? i : DateTime.MinValue;
            return rtn;
        }
    }
}
