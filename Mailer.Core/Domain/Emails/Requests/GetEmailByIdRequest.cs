using Ardalis.Result;
using Mailer.Core.Domain.Emails.Dtos;
using MediatR;

namespace Mailer.Core.Domain.Emails.Requests
{
    public  class GetEmailByIdRequest : IRequest<Result<EmailDto>>
    {
        public int EmailId { get; set; }

        public GetEmailByIdRequest(int draftEmailId)
        {
            EmailId = draftEmailId;
        }
    }
}
