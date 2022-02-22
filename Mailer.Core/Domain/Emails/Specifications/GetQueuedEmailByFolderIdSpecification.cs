using Ardalis.Specification;
using Mailer.Core.Domain.Folders;
using System.Linq;

namespace Mailer.Core.Domain.Emails.Specifications
{
    public class GetQueuedEmailByFolderIdSpecification : Specification<QueuedEmail>
    {


        public GetQueuedEmailByFolderIdSpecification(FolderType folderId, EmailPriority? emailPriority, string searchTerm)
        {
            var searchTermNormalized = searchTerm?.ToUpper();
            Query
                .Where(x => x.FolderId == folderId)
                .WhereIf(emailPriority.HasValue, x => x.EmailPriority == emailPriority)
                .WhereIf(searchTermNormalized.IsNotNullOrEmpty(), x => x.To.ToUpper().Contains(searchTermNormalized))
                .OrderByDescending(x => x.CreatedOnUtc);
        }
    }
}
