using AutoMapper;
using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Emails.Specifications;
using MediatR;


namespace Mailer.Core.Domain.Emails.Handlers
{
    public class GetEmailsPerFolderIdHandler : IRequestHandler<GetEmailsPerFolderIdRequest, List<EmailDto>>
    {

        private readonly IRepository<QueuedEmail> _emailRepository;
        private readonly IMapper _mapper;

        public GetEmailsPerFolderIdHandler(IRepository<QueuedEmail> emailRepository, IMapper mapper)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
        }
        public async Task<List<EmailDto>> Handle(GetEmailsPerFolderIdRequest request, CancellationToken cancellationToken)
        {
            var entityResult = await _emailRepository.ListAsync(new GetQueuedEmailByFolderIdSpecification(request.FolderType), cancellationToken);
            var mapperResult = _mapper.Map<List<EmailDto>>(entityResult);
            return mapperResult;
        }
    }
}
