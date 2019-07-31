namespace Officium.Ext
{
    public static class ObjectExt
    {
        public static T WithDefault<T>(this T obj, T defaultValue)
            where T : class
        {
            if (typeof(T) == typeof(string))
            {
                var s = obj as string;
                var rtn = string.IsNullOrEmpty(s) ? (defaultValue as string) : s;
                return rtn as T;
            }
            else
            {
                var rtn = obj != default(T) ? obj : defaultValue;
                return rtn;

            }
        }
    }
}
