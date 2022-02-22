using Ardalis.Specification;

namespace Mailer.Core.Domain.Emails.Specifications
{
    public class GetQueuedDraftEmailByIdSpecification : Specification<QueuedEmail>, ISingleResultSpecification
    {
        public GetQueuedDraftEmailByIdSpecification(int emailId, string currentUserEmail)
        {
            Query
                .Where(x => x.Id == emailId)
                .Where(x => x.FolderId == Folders.FolderType.Drafts)
                .Where(x => x.From == currentUserEmail);
        }
    }
}
