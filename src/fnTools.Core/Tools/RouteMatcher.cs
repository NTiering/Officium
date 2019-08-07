using fnTools.Core.ExtMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Officium.Core.Tools
{
    public class RouteMatcher
    {
        private readonly Regex bracketRemoveRegex;
        public RouteMatcher()
        {
            this.bracketRemoveRegex = new Regex(@"\{.+}");
        }
        public bool Matches(string source, string candidate)
        {
            var s = source.RemoveTrailingAndLeadingSlashes().ToUpper().Replace(@"/", @"\/"); ; 
            var c = candidate.RemoveTrailingAndLeadingSlashes().ToUpper();
            var regexString = "^"+ bracketRemoveRegex.Replace(s, ".+") + "$";
            var matcher = new Regex(regexString);
            var rtn = matcher.IsMatch(c);
            return rtn;
        }
    }
}
