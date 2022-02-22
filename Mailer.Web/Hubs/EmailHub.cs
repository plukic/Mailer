using Mailer.Core.Security.Users;
using Mailer.Web.HubClients;
using Microsoft.AspNetCore.SignalR;

namespace Mailer.Web.Hubs
{
    public class EmailHub : Hub<IEmailHubClient>
    {
       
    }
}
