namespace Mailer.Web.Infrastructure.UI.Alerts
{
    public interface IAlertManager
    {
        void Add(AlertType alert, string text);

        void Add(AlertType alert, string text, string title);

        void Add(AlertType alert, string text, string title, bool isDismissable);

        void Add(AlertItem alertItem);

        IEnumerable<AlertItem> GetAll();

        IEnumerable<AlertItem> GetByType(AlertType alertType);
    }
}