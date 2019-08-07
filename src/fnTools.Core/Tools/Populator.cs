using fnTools.Core.ExtMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Officium.Core.Tools
{
    public class Populator
    {
        
        public void Populate<T>(string key, string input)
            where T : class,new()
        {
            var intDict = ExtractIntermidiateDictionary(key, input);
            
        }

        private static IDictionary<string,string> ExtractIntermidiateDictionary(string key, string input)
        {
            var rtn = new Dictionary<string, string>();
            var keyParts = key.SplittIntoParts();
            var inputParts = input.SplittIntoParts();

            int count = -1;
            foreach (var k in keyParts)
            {
                count++;
                if (count > inputParts.Count()) continue;
                if (k.Contains("{") == false) continue;                
                var iKey = k.Replace("{", string.Empty).Replace("}", string.Empty);
                rtn[iKey] = inputParts[count];
            }

            return rtn;
        }

        public void Populate<T>(IDictionary<string,string> input)
              where T : class, new()
        {

        }
    }
}
