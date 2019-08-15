﻿using System.Collections.Generic;

namespace Officium.Tools.Handlers
{
    public class PathParamExtractor : IPathParamExtractor
    {
        public Dictionary<string, int> MakePathParams(string pathSelector)
        {
            var rtn = new Dictionary<string, int>();
            int count = 0;
            foreach (var i in pathSelector.Split("/"))
            {
                if (i.StartsWith("{") && i.EndsWith("}"))
                {
                    var key = i.Replace("{", string.Empty).Replace("}", string.Empty);
                    rtn[key] = count;
                }
                count++;
            }
            return rtn;
        }
    }
}