namespace Mailer.Web.Configuration
{
    public static class CookieNames
    {
        public const string Prefix = ".Mailer";
        public const string Authentication = Prefix + ".Auth";
        public const string TempData = Prefix + ".TempData";
        public const string AntiforgeryToken = Prefix + ".X-XSRF";
        public const string Culture = Prefix + ".Culture";
    }
}
