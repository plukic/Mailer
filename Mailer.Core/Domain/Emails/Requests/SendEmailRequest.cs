using Ardalis.Result;
using Mailer.Core.Domain.Emails.Dtos;
using MediatR;


namespace Mailer.Core.Domain.Emails.Requests
{
    public class SendEmailRequest : IRequest<Result<EmailDto>>
    {
        public SendEmailDto Data { get; set; }

        public SendEmailRequest(SendEmailDto data)
        {
            Data = data;
        }
    }
}
