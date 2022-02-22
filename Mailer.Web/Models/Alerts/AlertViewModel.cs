using Mailer.Web.Components;
using Mailer.Web.Infrastructure.UI.Alerts;

namespace Mailer.Web.Models.Alerts
{
    public class AlertViewModel
    {
        public IEnumerable<AlertItem> Alerts { get; set; }
        public AlertDisplayType DisplayType { get; set; }
    }
}
