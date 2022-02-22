using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Localization;
using Mailer.Web.Infrastructure.UI.Alerts;
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
        public async Task<IActionResult> Compose(MessageComposeViewModel model, string targetUpdate = null, int? draftEmailId = null)
        {
            if (!ModelState.IsValid)
            {
                var modelResult = await _emailViewModelService.PrepareMessageComposeModel(draftEmailId, model);
                return PartialView("_Compose", modelResult.Value);
            }

            var sendRequestDto = _emailViewModelService.PrepareSendMailRequest(draftEmailId, model);
            var sendMailResponse = await _mediator.Send(new SendEmailRequest(sendRequestDto));
            if (sendMailResponse.IsSuccess)
                _alertManager.AddSuccess(_localizer[LocalizationKeys.EmailSentSuccessfully]);
            else
                _alertManager.AddSuccess(_localizer[LocalizationKeys.ErrorWhileSendingEmail]);

            return AjaxRedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsDraft(MessageComposeViewModel model, string targetUpdate = null, int? draftEmailId = null)
        {
            if (!ModelState.IsValid)
            {
                var modelResult = await _emailViewModelService.PrepareMessageComposeModel(draftEmailId, model);
                return PartialView("_Compose", modelResult.Value);
            }

            var sendRequestDto = _emailViewModelService.PrepareSendMailRequest(draftEmailId, model);
            var sendMailResponse = await _mediator.Send(new SaveAsDraftRequest(sendRequestDto));
            if (sendMailResponse.IsSuccess)
                _alertManager.AddSuccess(_localizer[LocalizationKeys.EmailSavedAsDraftSuccessfully]);
            else
                _alertManager.AddSuccess(_localizer[LocalizationKeys.ErrorWhileSavingEmailAsDraft]);

            return AjaxRedirectToAction("Index", "Home");
        }
    }
}