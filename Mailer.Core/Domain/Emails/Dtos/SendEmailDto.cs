namespace Mailer.Core.Domain.Emails.Dtos
{
    public class SendEmailDto
    {
        public int? Id { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodyStriped { get; set; }
        public EmailPriority EmailPriority { get; set; }

    }
}
