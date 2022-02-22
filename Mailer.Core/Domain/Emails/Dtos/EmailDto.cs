using Mailer.Core.Base;
using Mailer.Core.Domain.Folders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Dtos
{
    public class EmailDto : BaseDto<int>
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
        public EmailPriority EmailPriority { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime SentOnUtc { get; set; }
        public FolderType FolderId { get; set; }
    }
}
