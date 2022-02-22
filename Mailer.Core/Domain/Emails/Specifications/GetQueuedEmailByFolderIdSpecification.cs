using Ardalis.Specification;
using Mailer.Core.Domain.Folders;
using System.Linq;

namespace Mailer.Core.Domain.Emails.Specifications
{
    public class GetQueuedEmailByFolderIdSpecification : Specification<QueuedEmail>
    {


        public GetQueuedEmailByFolderIdSpecification(FolderType folderId, string currentUserEmail, EmailPriority? emailPriority, string searchTerm, int page, int pageSize)
        {
            var searchTermNormalized = searchTerm?.ToUpper();
            Query
                .Where(x => x.FolderId == folderId)
                .Where(x => x.From == currentUserEmail)
                .WhereIf(emailPriority.HasValue, x => x.EmailPriority == emailPriority)
                .WhereIf(searchTermNormalized.IsNotNullOrEmpty(), x => x.To.ToUpper().Contains(searchTermNormalized))
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.CreatedOnUtc);
        }
    }
}
