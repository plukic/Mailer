using Mailer.Core.Authorization;
using Mailer.Core.Security.Users;
using Microsoft.AspNetCore.Mvc;
namespace Mailer.Web.Components
{
    public class NavigationMenu : ViewComponent
    {
        private static readonly Dictionary<string, string> _roleViewPairs = new Dictionary<string, string>
        {
            { ApplicationRoles.Admin, "Default" }
        };

        private readonly ICurrentUser _currentUser;

        public NavigationMenu(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool ignoreDisplayInNavigationMenu = false)
        {
            var model = new NavigationMenuViewModel();
            var defaultViewName = "Default";
            if (!_currentUser.Roles.Any())
                return View(defaultViewName, model);


            //Get view name with user role
            var viewName = _roleViewPairs.GetValueOrDefault(_currentUser.Roles.FirstOrDefault(), defaultViewName);
            return View(viewName, model);
        }
    }
}