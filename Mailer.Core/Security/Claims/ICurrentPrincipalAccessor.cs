using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Security.Claims
{
    /// <summary>
    /// Abstraction for current request <see cref="System.Security.Claims.ClaimsPrincipal"/>
    /// </summary>
    public interface ICurrentPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}
