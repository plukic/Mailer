using System.Security.Claims;

namespace Mailer.Core.Security.Claims
{
    public class MailerClaimTypes
    {
        public const string Name = "full_name";
        public const string Email = ClaimTypes.Email;
        public const string UserName = ClaimTypes.Name;
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string Role = ClaimTypes.Role;
    }
}
