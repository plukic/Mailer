using Mailer.Core.Domain.Folders;

namespace Mailer.Web.Components
{
    public class FolderViewModel
    {
        public FolderType FolderId { get; set; }
        public int MessagesCount { get; set; }
    }
}
