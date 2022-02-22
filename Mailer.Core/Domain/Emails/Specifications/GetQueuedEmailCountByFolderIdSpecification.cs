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
        public GetQueuedEmailCountByFolderIdSpecification(FolderType folderId)
        {
            Query.Where(x => x.FolderId == folderId);
        }
    }

}
