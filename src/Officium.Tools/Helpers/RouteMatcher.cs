using System.Text.RegularExpressions;

namespace Officium.Tools.Helpers
{
    public class RouteMatcher : IRouteMatcher
    {
        private readonly Regex bracketRemoveRegex;
        public RouteMatcher()
        {
            bracketRemoveRegex = new Regex(@"\{.+}");
        }
        public bool Matches(string source, string candidate)
        {
            var s = source.RemoveTrailingAndLeadingSlashes().ToUpper().Replace(@"/", @"\/"); ;
            var c = candidate.RemoveTrailingAndLeadingSlashes().ToUpper();
            var regexString = "^" + bracketRemoveRegex.Replace(s, ".+") + "$";
            var matcher = new Regex(regexString);
            var rtn = matcher.IsMatch(c);
            return rtn;
        }
    }
}
