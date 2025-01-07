using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Common
{
    public static class HttpContextExt
    {
        public static string GetUserId(this HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            return identity.FindFirst(c => c.Type == "UserId").Value;
        }

        public static string GetRole(this HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            return identity.FindFirst(c => c.Type == ClaimTypes.Role).Value;
        }
    }
}
