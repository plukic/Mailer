using AutoMapper;
using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Emails.Specifications;
using MediatR;
using X.PagedList;

namespace Mailer.Core.Domain.Emails.Handlers
{
    public class GetEmailsPerFolderIdHandler : IRequestHandler<GetEmailsPerFolderIdRequest, StaticPagedList<EmailDto>>
    {

        private readonly IRepository<QueuedEmail> _emailRepository;
        private readonly IMapper _mapper;

        public GetEmailsPerFolderIdHandler(IRepository<QueuedEmail> emailRepository, IMapper mapper)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
        }
        public async Task<StaticPagedList<EmailDto>> Handle(GetEmailsPerFolderIdRequest request, CancellationToken cancellationToken)
        {
            var pageIndex = request.Page - 1;//0 as default page
            var totalCountSpec = new GetQueuedEmailCountByFolderIdSpecification(request.FolderType, request.EmailPriority, request.SearchTerm);
            var filterTableSpec = new GetQueuedEmailByFolderIdSpecification(request.FolderType,
                request.EmailPriority,
                request.SearchTerm,
                pageIndex, request.PageSize);



            var totalCount = await _emailRepository.CountAsync(totalCountSpec);
            var entityResult = await _emailRepository.ListAsync(filterTableSpec);

            var mappedData = _mapper.Map<List<EmailDto>>(entityResult);
            var pagedResponse = new StaticPagedList<EmailDto>(mappedData, pageIndex + 1, request.PageSize, totalCount);

            return pagedResponse;
        }
    }
}
