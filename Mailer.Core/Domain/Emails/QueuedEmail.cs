using Mailer.Core.Base;
using Mailer.Core.Domain.Folders;

namespace Mailer.Core.Domain.Emails
{
    public class QueuedEmail : Entity
    {
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyToName { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodyStriped { get; set; }
        public EmailPriority EmailPriority { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? SentOnUtc { get; set; }
        public FolderType FolderId { get; set; }
    }
}
