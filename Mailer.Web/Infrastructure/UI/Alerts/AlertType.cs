namespace Mailer.Web.Infrastructure.UI.Alerts
{
    public enum AlertType
    {
        Success,
        Danger,
        Warning,
        Info
    }

    public static class AlertTypeExtensions
    {
        /// <summary>
        /// Returns proper class value based on enum <paramref name="value"/>
        /// </summary>
        public static string ToAlertTypeClass(this AlertType value)
        {
            return value switch
            {
                AlertType.Success => "success",
                AlertType.Danger => "danger",
                AlertType.Info => "info",
                AlertType.Warning => "warning",
                _ => string.Empty, // With margins between
            };
        }
    }
}
