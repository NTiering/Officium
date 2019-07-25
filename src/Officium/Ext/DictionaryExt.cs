using Microsoft.AspNetCore.Http;
using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Officium.Ext
{
    public static class DictionaryExt
    {
        public static void AddRange(this Dictionary<string, string> input, IQueryCollection query)
        {
            if (query == null) return;
            if (query.Keys == null) return;
            query.Keys.ToList().ForEach(x => {
                input[x] = query[x];
            });
        }

        public static void AddRange(this Dictionary<string, string> input, Dictionary<string, string> dict)
        {
            if (dict == null) return;
            if (dict.Keys == null) return;
            dict.Keys
               .ToList().ForEach(
               x => {
                   input[x] = dict[x];
               });
        }

        public static ICommand ToObject(this IDictionary<string, string> dict, Type type)
        {
            var t = Activator.CreateInstance(type);
            PropertyInfo[] properties = t.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                KeyValuePair<string, string> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                // Find which property type (int, string, double? etc) the CURRENT property is...
                Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

                // Fix nullables...
                Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

                // ...and change the type
                object newA = Convert.ChangeType(item.Value, newT);
                t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
            }
            return t as ICommand;
        }
    }
}
