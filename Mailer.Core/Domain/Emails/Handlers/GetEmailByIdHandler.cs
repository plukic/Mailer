using Ardalis.Result;
using AutoMapper;
using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Emails.Specifications;
using Mailer.Core.Security.Users;
using MediatR;

namespace Mailer.Core.Domain.Emails.Handlers
{
    public class GetEmailByIdHandler : IRequestHandler<GetEmailByIdRequest, Result<EmailDto>>
    {
        private IRepository<QueuedEmail> _repository;
        private IMapper _mapper;
        private ICurrentUser _currentUser;

        public GetEmailByIdHandler(IRepository<QueuedEmail> repository, IMapper mapper, ICurrentUser currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<EmailDto>> Handle(GetEmailByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetBySpecAsync(new GetQueuedEmailByIdSpecification(request.EmailId, _currentUser.Email));
            if (entity == null)
            {
                return Result<EmailDto>.NotFound();
            }
            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(entity));
        }
    }
}
