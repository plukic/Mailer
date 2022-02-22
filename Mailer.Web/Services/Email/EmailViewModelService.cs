using Ardalis.GuardClauses;
using Ardalis.Result;
using Mailer.Core.Domain.Emails;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Localization;
using Mailer.Web.Extensions;
using Mailer.Web.Models.Message;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Mailer.Web.Services.Email
{
    public class EmailViewModelService : IEmailViewModelService
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<EmailViewModelService> _localizer;

        public EmailViewModelService(IMediator mediator, IStringLocalizer<EmailViewModelService> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        public async Task<Result<MessageComposeViewModel>> PrepareMessageComposeModel(int? emailDraftId = null, MessageComposeViewModel model = null)
        {
            //new email request
            if (emailDraftId == null && model == null)
            {
                model = new MessageComposeViewModel();
            }
            //restore draft email request
            else if (emailDraftId != null && model == null)
            {
                model = new MessageComposeViewModel();
                var draftEmailResult = await _mediator.Send(new GetDraftEmailByIdRequest(emailDraftId.Value));
                if (!draftEmailResult.IsSuccess)
                    return Result<MessageComposeViewModel>.Error(_localizer[LocalizationKeys.EmailNotFound]);

                var draftEmail = draftEmailResult.Value;

                model.Id = draftEmail.Id;

                if (draftEmail.To.IsNotNullOrEmpty())
                    model.To = draftEmail.To.Split(",").ToList();
                if (draftEmail.Cc.IsNotNullOrEmpty())
                    model.Cc = draftEmail.Cc.Split(",").ToList();
                if (draftEmail.Bcc.IsNotNullOrEmpty())
                    model.Bcc = draftEmail.Bcc.Split(",").ToList();

                model.Subject = draftEmail.Subject;
                model.Body = draftEmail.Body;
                model.EmailPriority = draftEmail.EmailPriority;
            }

            model.EmailPriorities = PopulateEmailPriorities();
            return model;
        }

        public MessageComposeViewModel PrepareMessageComposeModel(EmailDto email)
        {
            Guard.Against.Null(email);
            var model = new MessageComposeViewModel();
            model.Id = email.Id;

            if (email.To.IsNotNullOrEmpty())
                model.To = email.To.Split(",").ToList();
            if (email.Cc.IsNotNullOrEmpty())
                model.Cc = email.Cc.Split(",").ToList();
            if (email.Bcc.IsNotNullOrEmpty())
                model.Bcc = email.Bcc.Split(",").ToList();

            model.Subject = email.Subject;
            model.Body = email.Body;
            model.EmailPriority = email.EmailPriority;
            return model;
        }

        public SendEmailDto PrepareSendMailRequest(int? draftEmailId, MessageComposeViewModel model)
        {
            return new SendEmailDto
            {
                Bcc = model.Bcc != null ? String.Join(",", model.Bcc) : null,
                Cc = model.Cc != null ? String.Join(",", model.Cc) : null,
                To = model.To != null ? String.Join(",", model.To) : null,
                EmailPriority = model.EmailPriority,
                Subject = model.Subject ?? _localizer[LocalizationKeys.NoSubject],
                BodyStriped = model.Body.StripHtml(),
                Body = model.Body,
                Id = draftEmailId
            };
        }

        private IList<SelectListItem> PopulateEmailPriorities()
        {
            var statusList = Enum.GetValues<EmailPriority>()
                                 .ToList()
                                 .ToSelectList(t => _localizer[t.ToLocalizationKey()]);
            return statusList;
        }
    }
}
