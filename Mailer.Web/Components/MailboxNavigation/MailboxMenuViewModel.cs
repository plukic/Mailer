namespace Mailer.Web.Components
{
    public class MailboxMenuViewModel
    {

        public FolderViewModel SentFolder { get; set; }
        public FolderViewModel DraftsFolder { get; set; }
        public FolderViewModel TrashFolder { get; set; }
        public string DataTargetUpdate { get; set; }
    }
}
