using Mailer.Core.Security.Users;
using Mailer.Web.HubClients;
using Microsoft.AspNetCore.SignalR;

namespace Mailer.Web.Hubs
{
    public class EmailHub : Hub<IEmailHubClient>
    {
        public Task JoinCloseDraftGroup(int id)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
        }
    }
}
