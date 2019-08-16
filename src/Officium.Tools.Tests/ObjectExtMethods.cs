using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Tools.Tests
{
    public static class ObjectExtMethods
    {
        public static T Cast<T>(this object o)
            where T : class
        {
            return o as T;
        }
    }
}
