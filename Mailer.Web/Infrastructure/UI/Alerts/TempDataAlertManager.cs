using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace Mailer.Web.Infrastructure.UI.Alerts
{
    public class TempDataAlertManager : IAlertManager
    {
        private const string TempDataKey = "_Alerts";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public TempDataAlertManager(
            IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        private void AddToDictionary(AlertItem alertItem)
        {
            if (alertItem is null)
            {
                throw new ArgumentNullException(nameof(alertItem));
            }

            var tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
            var alerts = tempData.ContainsKey(TempDataKey) ?
                   JsonSerializer.Deserialize<List<AlertItem>>(tempData[TempDataKey].ToString()) :
                   new List<AlertItem>();

            alerts.Add(alertItem);


            tempData[TempDataKey] = JsonSerializer.Serialize(alerts);
        }

        private List<AlertItem> GetFromDictionary()
        {
            var tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);

            return tempData.ContainsKey(TempDataKey) ?
                   JsonSerializer.Deserialize<List<AlertItem>>(tempData[TempDataKey].ToString()) :
                   new List<AlertItem>();
        }

        public void Add(AlertType alert, string text) => AddToDictionary(new AlertItem(alert, text));

        public void Add(AlertType alert, string text, string title) => AddToDictionary(new AlertItem(alert, text, title));

        public void Add(AlertType alert, string text, string title, bool isDismissable) => AddToDictionary(new AlertItem(alert, text, title, isDismissable));

        public void Add(AlertItem alertItem) => AddToDictionary(alertItem);

        public IEnumerable<AlertItem> GetAll() => GetFromDictionary();

        public IEnumerable<AlertItem> GetByType(AlertType alertType) => GetFromDictionary().Where(a => a.Type == alertType);
    }
}