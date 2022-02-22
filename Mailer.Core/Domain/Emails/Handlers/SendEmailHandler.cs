using Ardalis.Result;
using AutoMapper;
using Mailer.Core.Configuration;
using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Emails.Specifications;
using Mailer.Core.Security.Users;
using MediatR;

namespace Mailer.Core.Domain.Emails.Handlers
{
    public class SendEmailHandler : IRequestHandler<SendEmailRequest, Result<EmailDto>>
    {

        private IRepository<QueuedEmail> _repository;
        private IMapper _mapper;
        private IMediator _mediator;
        private ICurrentUser _currentUser;
        private EmailConfiguration _emailConfiguration;

        public SendEmailHandler(IRepository<QueuedEmail> repository, IMapper mapper, IMediator mediator, EmailConfiguration eailConfiguration, ICurrentUser currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _emailConfiguration = eailConfiguration;
            _currentUser = currentUser;
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
                From = _currentUser.Email,
                FromName = _currentUser.Name,
                ReplyTo = _emailConfiguration.ReplyTo,
                ReplyToName = _emailConfiguration.ReplyToName,
            };
            await _repository.AddAsync(newEmail);
            await _repository.SaveChangesAsync();
            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(newEmail));

        }

        private async Task<Result<EmailDto>> SendFromDraftEmail(SendEmailRequest request)
        {
            var draftEmail = await _repository.GetBySpecAsync(new GetQueuedDraftEmailByIdSpecification(request.Data.Id.Value, _currentUser.Email));
            if (draftEmail == null)
                return Result<EmailDto>.NotFound();

            draftEmail.To = request.Data.To;
            draftEmail.ToName = request.Data.To;

            draftEmail.Cc = request.Data.Cc;
            draftEmail.Bcc = request.Data.Bcc;
            draftEmail.Subject = request.Data.Subject;
            draftEmail.Body = request.Data.Body;
            draftEmail.FolderId = Folders.FolderType.Sent;
            draftEmail.EmailPriority = request.Data.EmailPriority;
            draftEmail.SentOnUtc = DateTime.UtcNow;

            await _repository.UpdateAsync(draftEmail);
            await _repository.SaveChangesAsync();
            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(draftEmail));

        }
    }
}
