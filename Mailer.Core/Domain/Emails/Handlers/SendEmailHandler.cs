using Ardalis.Result;
using AutoMapper;
using Mailer.Core.Configuration;
using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Emails.Specifications;
using MediatR;

namespace Mailer.Core.Domain.Emails.Handlers
{
    public class SendEmailHandler : IRequestHandler<SendEmailRequest, Result<EmailDto>>
    {

        private IRepository<QueuedEmail> _repository;
        private IMapper _mapper;
        private IMediator _mediator;
        private EmailConfiguration _emailConfiguration;

        public SendEmailHandler(IRepository<QueuedEmail> repository, IMapper mapper, IMediator mediator, EmailConfiguration eailConfiguration)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _emailConfiguration = eailConfiguration;
        }

        public async Task<Result<EmailDto>> Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            Result<EmailDto> result;
            if (request.Data.Id.HasValue)
            {
                result = await SendFromDraftEmail(request);
            }
            else
            {
                result = await SendNewEmail(request);
            }

            if (result.IsSuccess)
            {
                //Notify SignalR Users!
            }
            return result;
        }

        private async Task<Result<EmailDto>> SendNewEmail(SendEmailRequest request)
        {
            var newEmail = new QueuedEmail
            {
                To = request.Data.To,
                ToName = request.Data.To,
                Cc = request.Data.Cc,
                Bcc = request.Data.Bcc,
                Body = request.Data.Body,
                BodyStriped = request.Data.BodyStriped,
                CreatedOnUtc = DateTime.UtcNow,
                EmailPriority = request.Data.EmailPriority,
                FolderId = Folders.FolderType.Sent,
                SentOnUtc = DateTime.UtcNow,
                Subject = request.Data.Subject,
                From = _emailConfiguration.From,
                FromName = _emailConfiguration.FromName,
                ReplyTo = _emailConfiguration.ReplyTo,
                ReplyToName = _emailConfiguration.ReplyToName,
            };
            await _repository.AddAsync(newEmail);
            await _repository.SaveChangesAsync();
            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(newEmail));

        }

        private async Task<Result<EmailDto>> SendFromDraftEmail(SendEmailRequest request)
        {
            var draftEmail = await _repository.GetBySpecAsync(new GetQueuedDraftEmailByIdSpecification(request.Data.Id.Value));
            if (draftEmail == null)
                return Result<EmailDto>.NotFound();

            draftEmail.To = request.Data.To;
            draftEmail.Cc = request.Data.Cc;
            draftEmail.Bcc = request.Data.Bcc;
            draftEmail.Subject = request.Data.Subject;
            draftEmail.Body = request.Data.Body;
            draftEmail.EmailPriority = request.Data.EmailPriority;
            draftEmail.SentOnUtc = DateTime.UtcNow;

            await _repository.SaveChangesAsync();
            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(draftEmail));

        }
    }
}
