using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Officium.Ext
{
    public static class StringExt
    {
        public static string ValueAfter(this string s, string value)
        {
            var arr = s.Split('/').ToList();
            var pos = arr.FindIndex(x=>string.Compare(x,value,true) == 0);
            var rtn = arr[pos+1];
            return rtn;
        }

       
    }
}
