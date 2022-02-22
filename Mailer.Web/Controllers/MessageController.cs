using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Localization;
using Mailer.Web.Infrastructure.UI.Alerts;
using Mailer.Web.Models.Base;
using Mailer.Web.Models.Message;
using Mailer.Web.Services.Email;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Mailer.Web.Controllers
{
    [Authorize]
    public class MessageController : MailerBaseController
    {
        private readonly IMediator _mediator;
        private readonly IEmailViewModelService _emailViewModelService;
        private readonly IAlertManager _alertManager;
        private readonly IStringLocalizer<MessageController> _localizer;

        public MessageController(IMediator mediator, IEmailViewModelService emailViewModelService, IAlertManager alertManager, IStringLocalizer<MessageController> localizer)
        {
            _mediator = mediator;
            _emailViewModelService = emailViewModelService;
            _alertManager = alertManager;
            _localizer = localizer;
        }



        public async Task<IActionResult> Details(int emailId, string targetUpdate = null)
        {
            var emailResult = await _mediator.Send(new GetEmailByIdRequest(emailId));
            if (!emailResult.IsSuccess)
            {
                _alertManager.AddError(_localizer[LocalizationKeys.EmailNotFound]);
                return AjaxRedirectToAction("Index", "Home");
            }

            var model = _emailViewModelService.PrepareMessageComposeModel(emailResult.Value);
            model.TargetUpdate = targetUpdate;

            if (emailResult.Value.FolderId == Core.Domain.Folders.FolderType.Drafts)
                return PartialView("_Compose", model);
            else
                return PartialView("_Details", model);
        }
        public async Task<IActionResult> Compose(string targetUpdate = null, int? draftEmailId = null)
        {
            var modelResult = await _emailViewModelService.PrepareMessageComposeModel(draftEmailId);
            if (!modelResult.IsSuccess)
            {
                _alertManager.AddErrors(modelResult.Errors);
                return RedirectToAction("Index", "Home");
            }
            modelResult.Value.TargetUpdate = targetUpdate;
            return PartialView("_Compose", modelResult.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Compose(MessageComposeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var modelResult = await _emailViewModelService.PrepareMessageComposeModel(model.Id, model);
                return PartialView("_Compose", modelResult.Value);
            }

            var sendRequestDto = _emailViewModelService.PrepareSendMailRequest(model.Id, model);
            var sendMailResponse = await _mediator.Send(new SendEmailRequest(sendRequestDto));
            if (sendMailResponse.IsSuccess)
                _alertManager.AddSuccess(_localizer[LocalizationKeys.EmailSentSuccessfully]);
            else
                _alertManager.AddError(_localizer[LocalizationKeys.ErrorWhileSendingEmail]);

            return AjaxRedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsDraft(MessageComposeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var modelResult = await _emailViewModelService.PrepareMessageComposeModel(model.Id, model);
                return PartialView("_Compose", modelResult.Value);
            }

            var sendRequestDto = _emailViewModelService.PrepareSendMailRequest(model.Id, model);
            var sendMailResponse = await _mediator.Send(new SaveAsDraftRequest(sendRequestDto));
            if (sendMailResponse.IsSuccess)
                _alertManager.AddSuccess(_localizer[LocalizationKeys.EmailSavedAsDraftSuccessfully]);
            else
                _alertManager.AddError(_localizer[LocalizationKeys.ErrorWhileSavingEmailAsDraft]);

            return AjaxRedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_Delete", new BaseDeleteModel<int>
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, BaseDeleteModel<int> model)
        {

            var deleteEmailResponse = await _mediator.Send(new MoveEmailToTrashRequest(model.Id));
            if (deleteEmailResponse.IsSuccess)
                _alertManager.AddSuccess(_localizer[LocalizationKeys.EmailDeletedSuccessfully]);
            else
                _alertManager.AddError(_localizer[LocalizationKeys.ErrorWhileDeletingEmail]);
            return AjaxRedirectToAction("Index", "Home");
        }



    }
}