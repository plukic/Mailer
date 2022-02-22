using Mailer.Core.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Security.Users
{
    /// <summary>
    /// Current user implementation
    /// </summary>
    public class CurrentUser : ICurrentUser
    {
        /// <summary>
        /// Static empty array for optimization
        /// </summary>
        private static readonly Claim[] EmptyClaims = Array.Empty<Claim>();

        /// <summary>
        /// Current claim principal
        /// </summary>
        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

        public CurrentUser(ICurrentPrincipalAccessor currentPrincipalAccessor)
        {
            _currentPrincipalAccessor = currentPrincipalAccessor;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Id);

        public string UserName => this.FindClaimValue(ClaimTypes.Name);

        public string? Id => _currentPrincipalAccessor.Principal.GetUserId();

        public string[] Roles => FindClaims(ClaimTypes.Role).Select(c => c.Value).ToArray();

        public Claim? FindClaim(string claimType) =>
            _currentPrincipalAccessor.Principal?.FindFirst(claimType);

        public Claim[] FindClaims(string claimType) =>
            _currentPrincipalAccessor.Principal?.FindAll(claimType).ToArray() ?? EmptyClaims;

        public Claim[] GetAllClaims() =>
            _currentPrincipalAccessor.Principal?.Claims.ToArray() ?? EmptyClaims;

        public bool IsInRole(string roleName) =>
            _currentPrincipalAccessor.Principal.IsInRole(roleName);
    }
}
