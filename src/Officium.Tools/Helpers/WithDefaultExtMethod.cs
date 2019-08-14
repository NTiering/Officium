using System;

namespace Officium.Tools.Helpers
{
    public static class WithDefaultExt
    {
        public static string WithDefault(string s, string defaultValue)
        {
            var rtn = s.IsNullOrWhitespace() ? defaultValue : s;
            return rtn;
        }

        public static int WithDefault(int i, int defaultValue)
        {
            var rtn = i == 0 ? defaultValue : i;
            return rtn;
        }

        public static DateTime WithDefault(DateTime dt, DateTime defaultValue)
        {
            var rtn = dt == DateTime.MinValue ? defaultValue : dt;
            return rtn;
        }
    }
}
