using System.Text.Json.Serialization;

namespace Mailer.Web.Infrastructure.UI.Alerts
{
    public class AlertItem
    {
        #region Constructors

        // Attribut needed for (de)serialization
        [JsonConstructor]
        public AlertItem(AlertType Type, string Text)
          : this(Type, Text, null, true)
        {
        }

        public AlertItem(AlertType type, string text, string title)
            : this(type, text, title, true)
        {
        }

        public AlertItem(AlertType type, string text, string title, bool isDismissable)
        {
            Text = text;
            Title = title;
            IsDismissable = isDismissable;
            Type = type;
        }

        #endregion Constructors

        public string Text { get; set; }
        public string Title { get; set; }
        public bool IsDismissable { get; set; }
        public AlertType Type { get; set; }
    }
}
