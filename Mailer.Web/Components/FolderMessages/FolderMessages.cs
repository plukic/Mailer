using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Folders;
using Mailer.Core.Security.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mailer.Web.Components
{
    public class FolderMessages : ViewComponent
    {
        private readonly IMediator _mediator;

        public FolderMessages( IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(FolderType folderId)
        {
            var model = new FolderMessagesViewModel();
            model.FolderId = folderId;
            model.FolderName = folderId.ToString();
            var result = await _mediator.Send(new GetEmailsPerFolderIdRequest(folderId));
            model.Emails = result;
            return View(model);
        }
    }
}
