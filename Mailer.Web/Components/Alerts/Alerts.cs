using Mailer.Web.Infrastructure.UI.Alerts;
using Mailer.Web.Models.Alerts;
using Microsoft.AspNetCore.Mvc;

namespace Mailer.Web.Components

{
    public class Alerts : ViewComponent
    {
        private readonly IAlertManager _alertManager;

        public Alerts(IAlertManager alertManager)
        {
            _alertManager = alertManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(AlertDisplayType displayType = AlertDisplayType.Default)
        {
            var model = new AlertViewModel
            {
                Alerts = _alertManager.GetAll(),
                DisplayType = displayType
            };

            return await Task.FromResult<IViewComponentResult>(View(model));
        }
    }

    public enum AlertDisplayType
    {
        Default = 0,
        Stacked = 1
    }

    public static class ModalSizeExtensions
    {
        /// <summary>
        /// Returns proper insertion value based on enum <paramref name="value"/>
        /// </summary>
        /// <param name="value">Insertion mode</param>
        /// <returns></returns>
        public static string ToAlertDisplayTypeClass(this AlertDisplayType value)
        {
            return value switch
            {
                AlertDisplayType.Stacked => "alert-stacked", // No margins between
                _ => string.Empty, // With margins between
            };
        }
    }
}