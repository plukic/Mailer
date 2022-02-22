using Ardalis.Result;
using AutoMapper;
using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Notifications;
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
    public class MoveEmailToTrashHandler : IRequestHandler<MoveEmailToTrashRequest, Result<EmailDto>>
    {
        private IRepository<QueuedEmail> _repository;
        private IMapper _mapper;
        private IMediator _mediator;
        private ICurrentUser _currentUser;

        public MoveEmailToTrashHandler(IRepository<QueuedEmail> repository, IMapper mapper, ICurrentUser currentUser, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
            _mediator = mediator;
        }
        public async Task<Result<EmailDto>> Handle(MoveEmailToTrashRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetBySpecAsync(new GetQueuedEmailByIdSpecification(request.EmailId, _currentUser.Email));
            if (entity == null)
                return Result<EmailDto>.NotFound();

            entity.FolderId = Folders.FolderType.Trash;

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            await _mediator.Publish(new RefreshFolderNumbersNotification());
            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(entity));
        }
    }
}
