using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Folders;

namespace Mailer.Web.Components
{
    public class FolderMessagesViewModel
    {

        public string TargetUpdate { get; set; }
        public FolderType FolderId { get; set; }
        public string FolderName { get; set; }

        public List<EmailDto> Emails { get; set; }

    }
}
