using Mailer.Core.Domain.Emails;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Folders;
using Mailer.Core.Localization;
using Mailer.Core.Security.Users;
using Mailer.Web.Extensions;
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

        public async Task<IViewComponentResult> InvokeAsync(
            FolderType folderId,
            string targetUpdate = null,
            string searchTerm = null,
            EmailPriority? emailPriority = null)
        {
            var model = new FolderMessagesViewModel();
            model.FolderId = folderId;
            model.FolderName = _localizer[folderId.ToLocalizationKey()];
            model.TargetUpdate = targetUpdate;
            model.EmailPriority = emailPriority;
            model.SearchTerm = searchTerm;

            model.EmailPriorities = Enum.GetValues<EmailPriority>()
                            .ToList()
                            .ToSelectList(t => _localizer[t.ToLocalizationKey()]).ToList();

            model.EmailPriorities.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = _localizer[LocalizationKeys.All]
            });
            var result = await _mediator.Send(new GetEmailsPerFolderIdRequest(folderId, emailPriority, searchTerm));
            model.Emails = result;
            return View(model);
        }
    }
}
