using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Mailer.Web.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IAuthorizationService"/>
    /// </summary>
    public static class AuthorizationServiceExtensions
    {
        public static async Task<bool> IsAuthorizedAsync(this IAuthorizationService authorizationService,
            ClaimsPrincipal user,
            object resource,
            IAuthorizationRequirement requirement)
            => (await authorizationService.AuthorizeAsync(user, resource, requirement)).Succeeded;

        public static async Task<bool> IsAuthorizedAsync(this IAuthorizationService authorizationService,
            ClaimsPrincipal user,
            string policyName)
            => (await authorizationService.AuthorizeAsync(user, policyName)).Succeeded;
    }
}
