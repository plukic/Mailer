using Mailer.Web.Models.Ajax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Text.Json;

namespace Mailer.Web.Controllers
{
    public class MailerBaseController : Controller
    {

        #region Ajax redirection

        protected IActionResult AjaxRedirectToAction(string action)
            => AjaxRedirectToAction(action, null);

        protected IActionResult AjaxRedirectToAction(string action, object values)
            => AjaxRedirectToAction(action, null, values);

        protected IActionResult AjaxRedirectToAction(string action, string controller)
            => AjaxRedirectToAction(action, controller, null);

        protected IActionResult AjaxRedirectToAction(string action, string controller, object values)
            => AjaxJson(new AjaxResponse(Url.Action(action, controller, values)));

        protected IActionResult AjaxRedirectToHomePage()
            => AjaxJson(new AjaxResponse(Url.Content("~/")));

     
        #endregion Ajax redirection

        #region Ajax helper results

        /// <summary>
        /// Since we use camel case notation in JS code, we will use central method with CamelCase notation.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Json</returns>
        protected IActionResult AjaxJson(object data)
        {
            return Json(data, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// Since select2 binds options by object named "results" we will assign our data to ano results object.
        /// If we don't do it this we will have to write manual projections.
        /// <see cref="https://select2.org/data-sources/ajax#transforming-response-data"/>
        /// </summary>
        protected IActionResult Select2Json(object data)
        {
            return Json(new { results = data }, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        protected IActionResult AjaxSuccess<T>(T result)
        {
            return Json(new AjaxResponse<T>(result), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        protected IActionResult AjaxSuccess(LocalizedString locale)
        {
            return Json(new AjaxResponse<string>(result: locale.Value), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        protected IActionResult AjaxSuccess()
        {
            return Json(new AjaxResponse(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// Returns response with Ajax model
        /// Note that we manually set status code to 500 so correct handlers can be triggered
        /// </summary>
        protected IActionResult AjaxError(ErrorInfo error)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;

            return Json(new AjaxResponse(error), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        #endregion Ajax helper results

     
    }
}
