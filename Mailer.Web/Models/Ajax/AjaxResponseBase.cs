namespace Mailer.Web.Models.Ajax
{
    public abstract class AjaxResponseBase
    {
        public string RedirectUrl { get; set; }

        public bool Success { get; set; }

        public ErrorInfo Error { get; set; }
    }
}
