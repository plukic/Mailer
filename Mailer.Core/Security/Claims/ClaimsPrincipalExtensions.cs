using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirst(MailerClaimTypes.UserId);

            return claim?.Value;
        }

        public static string? GetUserName(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirst(MailerClaimTypes.UserName);

            return claim?.Value;
        }

        public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
        {
            var claims = principal.FindAll(MailerClaimTypes.Role).Select(s => s.Value);

            return claims ?? Enumerable.Empty<string>();
        }
    }
}
