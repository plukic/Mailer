using System.Security.Claims;

namespace Mailer.Core.Security.Users
{
    /// <summary>
    /// Abstracts currently logged-in user
    /// *Define only base members and extend with extension methods when necessary
    /// </summary>
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        string UserName { get; }
        string Email { get; }
        string Name { get; }
        string? Id { get; }
        string[] Roles { get; }

        Claim? FindClaim(string claimType);

        Claim[] FindClaims(string claimType);

        Claim[] GetAllClaims();

        bool IsInRole(string roleName);
    }
}
