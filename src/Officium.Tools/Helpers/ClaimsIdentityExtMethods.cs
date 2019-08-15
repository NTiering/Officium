using System.Linq;
using System.Security.Claims;

namespace Officium.Tools.Helpers
{
    public static class ClaimsIdentityExtMethods
    {
        public static bool HasClaim(this ClaimsIdentity claimsIdentity, Claim claim)
        {
            if (claimsIdentity == null) return false;
            if (claim == null) return false;
            if (claimsIdentity.Claims == null) return false;
            var rtn = claimsIdentity.Claims.Any(x => x.Type == claim.Type && x.Value == claim.Value);
            return rtn;
        }
    }
}
