using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Folders;
using Mailer.Core.Security.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Mailer.Web.Components
{
    public class FolderMessages : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<FolderMessages> _localizer;

        public FolderMessages(IMediator mediator, IStringLocalizer<FolderMessages> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        public async Task<IViewComponentResult> InvokeAsync(FolderType folderId,string targetUpdate)
        {
            var model = new FolderMessagesViewModel();
            model.FolderId = folderId;
            model.FolderName = _localizer[folderId.ToLocalizationKey()];
            model.TargetUpdate = targetUpdate;
            var result = await _mediator.Send(new GetEmailsPerFolderIdRequest(folderId));
            model.Emails = result;
            return View(model);
        }
    }
}
