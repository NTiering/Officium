using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Plugins
{
    public static class ExtentionMethods
    {
        public static T WithDefault<T>(this T item, T defaultValue)
        {
            var rtn = item.Equals( default(T)) ? defaultValue : item;
            return rtn;
        }

        public static int AsInt(this string s)
        {
            var t = 0;
            var rtn = int.TryParse(s, out t) ? t : 0;
            return rtn;
        }

        public static DateTime AsDateTime(this string s)
        {
            DateTime t ;
            var rtn = DateTime.TryParse(s, out t) ? t : DateTime.MinValue;
            return rtn;
        }
    }
}
