using Ardalis.Result;
using Mailer.Core.Domain.Emails.Dtos;
using MediatR;

namespace Mailer.Core.Domain.Emails.Requests
{
    public  class GetDraftEmailByIdRequest : IRequest<Result<EmailDto>>
    {
        public int DraftEmailId { get; set; }

        public GetDraftEmailByIdRequest(int draftEmailId)
        {
            DraftEmailId = draftEmailId;
        }
    }
}
