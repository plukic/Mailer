using Mailer.Core.Data;
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
    public class GetEmailsCountPerFolderIdHandler : IRequestHandler<GetEmailsCountPerFolderIdRequest, int>
    {
        private IRepository<QueuedEmail> _repository;
        private ICurrentUser _currentUser;

        public GetEmailsCountPerFolderIdHandler(IRepository<QueuedEmail> repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public Task<int> Handle(GetEmailsCountPerFolderIdRequest request, CancellationToken cancellationToken)
        {
            return _repository.CountAsync(new GetQueuedEmailCountByFolderIdSpecification(request.FolderType, _currentUser.Email));
        }
    }
}
