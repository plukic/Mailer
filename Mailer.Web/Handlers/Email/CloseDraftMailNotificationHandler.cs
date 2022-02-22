using Mailer.Core.Domain.Emails.Notifications;
using Mailer.Core.Security.Users;
using Mailer.Web.HubClients;
using Mailer.Web.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Mailer.Web.Handlers.Email
{
    public class CloseDraftMailNotificationHandler : INotificationHandler<CloseDraftMailNotification>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        private readonly IHubContext<EmailHub, IEmailHubClient> _hubContext;

        public CloseDraftMailNotificationHandler(IMediator mediator, IHubContext<EmailHub, IEmailHubClient> hubContext,
            ICurrentUser currentUser)
        {
            _mediator = mediator;
            _hubContext = hubContext;
            _currentUser = currentUser;
        }

        public async Task Handle(CloseDraftMailNotification notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.Group(notification.Id.ToString()).CloseDraft();
        }
    }
}
