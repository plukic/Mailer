using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Security.Users
{
    /// <summary>
    /// Additional extension methods for <see cref="ICurrentUser"/>
    /// </summary>
    public static class CurrentUserExtensions
    {
        public static string FindClaimValue(this ICurrentUser currentUser, string claimType)
            => currentUser.FindClaim(claimType)?.Value;

        public static T? FindClaimValue<T>(this ICurrentUser currentUser, string claimType) where T : struct
        {
            var claim = currentUser.FindClaim(claimType);

            return claim != null ? claim.Value.To<T>() : null;
        }
    }
}
