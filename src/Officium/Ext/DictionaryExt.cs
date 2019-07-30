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
        //

        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> input, PaginationRequest req)
        {
            var rtn = input.Skip(req.PageSize * req.PageNum).Take(req.PageSize);
            return rtn;
        }
        public static Dictionary<string, string> AddRange(this Dictionary<string, string> input, IQueryCollection query)
        {
            if (query == null) return input;
            if (query.Keys == null) return input;
            query.Keys
                .ToList()
                .ForEach(x =>
            {
                input[x] = query[x];
            });

            return input;
        }

        public static Dictionary<string, string> AddRange(this Dictionary<string, string> input, Dictionary<string, string> dict)
        {
            if (dict == null) return input;
            if (dict.Keys == null) return input;
            dict.Keys
               .ToList()
               .ForEach(x =>
               {
                   input[x] = dict[x];
               });
            return input;
        }

        public static PaginationRequest ToPaginationRequest(this IDictionary<string, string> dict)
        {
            var rtn = new PaginationRequest();
            PopulateObject(dict, rtn);
            rtn.SetDefaults();
            return rtn;
        }

        public static ICommand ToObject(this IDictionary<string, string> dict, Type type)
        {
            var command = Activator.CreateInstance(type);
            if (dict == null) return command as ICommand;
            PopulateObject(dict, command);
            return command as ICommand;
        }

        private static void PopulateObject<T>(IDictionary<string, string> dict, T tCmd)
        {
            var properties = tCmd.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                var item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));
                var tPropertyType = tCmd.GetType().GetProperty(property.Name).PropertyType;
                var newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;
                var newA = new object();
                try
                {
                    newA = Convert.ChangeType(item.Value, newT);
                    tCmd.GetType().GetProperty(property.Name).SetValue(tCmd, newA, null);
                }
                catch
                {
                   // dont try to set it 
                }               
                
            }
        }
    }

    public sealed class PaginationRequest
    {
        public int PageNum { get; internal set; }
        public int PageSize { get; internal set; }
        internal PaginationRequest()
        {
        }
      

        internal PaginationRequest SetDefaults()
        {            
            PageSize = (PageSize == 0) ? 25 : PageSize;
            return this;
        }
    }
}
