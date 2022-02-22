using Ardalis.GuardClauses;

namespace Mailer.Web.Infrastructure.UI.Alerts
{
    public static class AlertManagerExtensions
    {
        public static void AddErrors(this IAlertManager alertManager, IEnumerable<string> errors)
        {
            Guard.Against.Null(alertManager, nameof(alertManager));

            foreach (var error in errors)
            {
                alertManager.AddError(error);
            }
        }


        public static void AddError(this IAlertManager alertManager, string text)
        {
            Guard.Against.Null(alertManager, nameof(alertManager));

            alertManager.Add(new AlertItem(AlertType.Danger, text));
        }

        public static void AddInfo(this IAlertManager alertManager, string text)
        {
            Guard.Against.Null(alertManager, nameof(alertManager));

            alertManager.Add(new AlertItem(AlertType.Info, text));
        }

        public static void AddSuccess(this IAlertManager alertManager, string text)
        {
            Guard.Against.Null(alertManager, nameof(alertManager));

            alertManager.Add(new AlertItem(AlertType.Success, text));
        }

        public static void AddWarning(this IAlertManager alertManager, string text)
        {
            Guard.Against.Null(alertManager, nameof(alertManager));

            alertManager.Add(new AlertItem(AlertType.Warning, text));
        }
    }
}
