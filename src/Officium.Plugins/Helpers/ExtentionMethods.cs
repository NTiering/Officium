namespace Officium.Plugins.Helpers
{
    using System;
    public static class ExtentionMethods
    {
        public static T WithDefault<T>(this T item, T defaultValue)
        {
            var rtn = item.Equals(default(T)) ? defaultValue : item;
            return rtn;
        }
        public static bool Is(this string s, string compare)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            return string.Compare(s, compare, true) == 0;
        }

        public static int AsInt(this string s)
        {
            var rtn = int.TryParse(s, out int t) ? t : 0;
            return rtn;
        }

        public static DateTime AsDateTime(this string s)
        {
            var rtn = DateTime.TryParse(s, out DateTime t) ? t : DateTime.MinValue;
            return rtn;
        }
    }
}
