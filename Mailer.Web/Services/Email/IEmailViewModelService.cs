using Ardalis.Result;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Web.Models.Message;

namespace Mailer.Web.Services.Email
{
    public interface IEmailViewModelService
    {
        Task<Result<MessageComposeViewModel>> PrepareMessageComposeModel(int? emailDraftId = null, MessageComposeViewModel model = null);
        MessageComposeViewModel PrepareMessageComposeModel(EmailDto email);
        SendEmailDto PrepareSendMailRequest(int? draftEmailId, MessageComposeViewModel model);
    }
}
