using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;

namespace Mailer.Web.Services.ExternalLogin
{
    public interface IExternalLoginService
    {
        List<Claim> GetOrCreateClaimsForExternalLogin(TokenValidatedContext ctx);

    }
}
