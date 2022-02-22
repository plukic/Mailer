using Ardalis.Specification;
using Mailer.Core.Domain.Folders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Specifications
{
    internal class GetQueuedEmailCountByFolderIdSpecification : Specification<QueuedEmail>
    {
        public GetQueuedEmailCountByFolderIdSpecification(FolderType folderId, 
            EmailPriority? emailPriority = null, 
            string searchTerm = null)
        {
            var searchTermNormalized = searchTerm?.ToUpper();
            Query
                .Where(x => x.FolderId == folderId)
                .WhereIf(emailPriority.HasValue, x => x.EmailPriority == emailPriority)
                .WhereIf(searchTermNormalized.IsNotNullOrEmpty(), x => x.To.ToUpper().Contains(searchTermNormalized));
        }
    }

}
