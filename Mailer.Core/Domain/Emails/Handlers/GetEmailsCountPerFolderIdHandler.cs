using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Emails.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Handlers
{
    public class GetEmailsCountPerFolderIdHandler : IRequestHandler<GetEmailsCountPerFolderIdRequest, int>
    {
        private IRepository<QueuedEmail> _repository;

        public GetEmailsCountPerFolderIdHandler(IRepository<QueuedEmail> repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(GetEmailsCountPerFolderIdRequest request, CancellationToken cancellationToken)
        {
            return _repository.CountAsync(new GetQueuedEmailCountByFolderIdSpecification(request.FolderType));
        }
    }
}
