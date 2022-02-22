using Mailer.Core.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;

namespace Mailer.Web.Services.ExternalLogin
{
    public class Auth0ExternalLoginService : IExternalLoginService
    {

        public List<Claim> GetOrCreateClaimsForExternalLogin(TokenValidatedContext ctx)
        {

            //we can use this later as a external Id
            string uniqueId = ctx.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            //for demo lets say that nickname is both email and username
            string email = ctx.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            string fullName = ctx.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value + " " +
                ctx.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;

            return new List<Claim>() {
                new Claim(MailerClaimTypes.Email, email),
                new Claim(MailerClaimTypes.UserName, email),
                new Claim(MailerClaimTypes.Name, fullName ),
            };
        }
    }
}