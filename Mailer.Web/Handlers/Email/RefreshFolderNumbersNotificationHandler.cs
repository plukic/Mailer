using Mailer.Core.Domain.Emails.Notifications;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Folders;
using Mailer.Core.Security.Users;
using Mailer.Web.HubClients;
using Mailer.Web.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Mailer.Web.Handlers.Email
{
    public class RefreshFolderNumbersNotificationHandler : INotificationHandler<RefreshFolderNumbersNotification>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        private readonly IHubContext<EmailHub,IEmailHubClient> _hubContext;

        public RefreshFolderNumbersNotificationHandler(IMediator mediator, IHubContext<EmailHub, IEmailHubClient> hubContext, ICurrentUser currentUser)
        {
            _mediator = mediator;
            _hubContext = hubContext;
            _currentUser = currentUser;
        }

        public async Task Handle(RefreshFolderNumbersNotification notification, CancellationToken cancellationToken)
        {
            var sentCount = await _mediator.Send(new GetEmailsCountPerFolderIdRequest(FolderType.Sent));
            var draftsCount = await _mediator.Send(new GetEmailsCountPerFolderIdRequest(FolderType.Drafts));
            var trashCount = await _mediator.Send(new GetEmailsCountPerFolderIdRequest(FolderType.Trash));

            //just for demo
            //in prod this should refactor to one method
            await _hubContext.Clients.User(_currentUser.Id).RefreshSent(sentCount);
            await _hubContext.Clients.User(_currentUser.Id).RefreshDraft(draftsCount);
            await _hubContext.Clients.User(_currentUser.Id).RefreshTrash(trashCount);
        }
    }
}
