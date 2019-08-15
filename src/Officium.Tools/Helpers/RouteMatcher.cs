namespace Officium.Tools.Helpers
{
    using System.Text.RegularExpressions;

    public class RouteMatcher : IRouteMatcher
    {
        private readonly Regex _bracketRemoveRegex;

        public RouteMatcher()
        {
            _bracketRemoveRegex = new Regex(@"\{.+}");
        }

        public bool Matches(string source, string candidate)
        {
            var s = source.RemoveTrailingAndLeadingSlashes().ToUpper().Replace(@"/", @"\/"); ;
            var c = candidate.RemoveTrailingAndLeadingSlashes().ToUpper();
            var regexString = "^" + _bracketRemoveRegex.Replace(s, ".+") + "$";
            var matcher = new Regex(regexString);
            var rtn = matcher.IsMatch(c);
            return rtn;
        }
    }
}
