namespace Officium.Ext
{
    using Microsoft.AspNetCore.Http;
    using Officium.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class DictionaryExt
    {
        public static void AddRange(this Dictionary<string, string> input, IQueryCollection query)
        {
            if (query == null) return;
            if (query.Keys == null) return;
            query.Keys
                .ToList()
                .ForEach(x =>
            {
                input[x] = query[x];
            });
        }

        public static void AddRange(this Dictionary<string, string> input, Dictionary<string, string> dict)
        {
            if (dict == null) return;
            if (dict.Keys == null) return;
            dict.Keys
               .ToList()
               .ForEach(x =>
               {
                   input[x] = dict[x];
               });
        }

        public static ICommand ToObject(this IDictionary<string, string> dict, Type type)
        {
            var tCmd = Activator.CreateInstance(type);
            if (dict == null) return tCmd as ICommand;

            var properties = tCmd.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                var item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));
                var tPropertyType = tCmd.GetType().GetProperty(property.Name).PropertyType;
                var newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;
                var newA = Convert.ChangeType(item.Value, newT);
                tCmd.GetType().GetProperty(property.Name).SetValue(tCmd, newA, null);
            }
            return tCmd as ICommand;
        }
    }
}
