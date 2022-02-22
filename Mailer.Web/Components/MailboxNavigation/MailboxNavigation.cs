using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Folders;
using Mailer.Core.Security.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mailer.Web.Components
{
    public class MailboxNavigation : ViewComponent
    {
        private readonly IMediator _mediator;

        public MailboxNavigation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(string targetUpdate)
        {
            var model = new MailboxMenuViewModel();
            
            model.DraftsFolder = new FolderViewModel { MessagesCount = 0, FolderId = Core.Domain.Folders.FolderType.Drafts };
            model.SentFolder = new FolderViewModel { MessagesCount = 0, FolderId = Core.Domain.Folders.FolderType.Sent };
            model.TrashFolder = new FolderViewModel { MessagesCount = 0, FolderId = Core.Domain.Folders.FolderType.Trash };

            model.DraftsFolder.MessagesCount = await _mediator.Send(new GetEmailsCountPerFolderIdRequest(FolderType.Drafts));
            model.SentFolder.MessagesCount = await _mediator.Send(new GetEmailsCountPerFolderIdRequest(FolderType.Sent));
            model.TrashFolder.MessagesCount = await _mediator.Send(new GetEmailsCountPerFolderIdRequest(FolderType.Trash));


            model.DataTargetUpdate = targetUpdate;
            return View(model);
        }
    }
}
