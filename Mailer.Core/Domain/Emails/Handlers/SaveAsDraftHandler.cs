using Ardalis.Result;
using AutoMapper;
using Mailer.Core.Configuration;
using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Emails.Specifications;
using Mailer.Core.Security.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Handlers
{
    public class SaveAsDraftHandler : IRequestHandler<SaveAsDraftRequest, Result<EmailDto>>
    {
        private IRepository<QueuedEmail> _repository;
        private IMapper _mapper;
        private IMediator _mediator;
        private ICurrentUser _currentUser;
        private EmailConfiguration _emailConfiguration;

        public SaveAsDraftHandler(IRepository<QueuedEmail> repository, IMapper mapper, IMediator mediator, EmailConfiguration emailConfiguration, ICurrentUser currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _emailConfiguration = emailConfiguration;
            _currentUser = currentUser;
        }

        public async Task<Result<EmailDto>> Handle(SaveAsDraftRequest request, CancellationToken cancellationToken)
        {
            var data = request.Data;
            Result<EmailDto> result;
            if (data.Id.HasValue)
            {
                result = await UpdateExistingDraftMail(data);
            }
            else
            {
                result = await CreateNewDraftMail(data);
            }

            return result;
        }

        private async Task<Result<EmailDto>> CreateNewDraftMail(SendEmailDto data)
        {
            var newEmail = new QueuedEmail
            {
                To = data.To,
                ToName = data.To,
                Cc = data.Cc,
                Bcc = data.Bcc,
                Body = data.Body,
                BodyStriped = data.BodyStriped,
                CreatedOnUtc = DateTime.UtcNow,
                EmailPriority = data.EmailPriority,
                FolderId = Folders.FolderType.Drafts,
                SentOnUtc = null,
                Subject = data.Subject,

                From = _currentUser.Email,
                FromName = _currentUser.Name,

                ReplyTo = _emailConfiguration.ReplyTo,
                ReplyToName = _emailConfiguration.ReplyToName,
            };
            await _repository.AddAsync(newEmail);
            await _repository.SaveChangesAsync();
            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(newEmail));
        }

        private async Task<Result<EmailDto>> UpdateExistingDraftMail(SendEmailDto data)
        {
            var draftEmail = await _repository.GetBySpecAsync(new GetQueuedDraftEmailByIdSpecification(data.Id.Value, _currentUser.Email));
            if (draftEmail == null)
                return Result<EmailDto>.NotFound();

            draftEmail.To = data.To;
            draftEmail.ToName = data.To;
            draftEmail.Cc = data.Cc;
            draftEmail.Bcc = data.Bcc;
            draftEmail.Subject = data.Subject;
            draftEmail.Body = data.Body;
            draftEmail.BodyStriped = data.BodyStriped;
            draftEmail.EmailPriority = data.EmailPriority;

            await _repository.SaveChangesAsync();
            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(draftEmail));
        }
    }
}
