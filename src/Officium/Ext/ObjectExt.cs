namespace Officium.Ext
{
    public static class ObjectExt
    {
        public static T WithDefault<T>(this T obj, T defaultValue)
            where T : class
        {
            var rtn = obj != default(T) ? obj : defaultValue;
            return rtn;
        }
    }
}
