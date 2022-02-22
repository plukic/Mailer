using Ardalis.Specification;
using Mailer.Core.Domain.Folders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Specifications
{
    public class GetQueuedEmailByFolderIdSpecification : Specification<QueuedEmail>
    {
        public GetQueuedEmailByFolderIdSpecification(FolderType folderId)
        {
            Query.Where(x => x.FolderId == folderId).OrderByDescending(x=>x.CreatedOnUtc);
        }
    }
}
