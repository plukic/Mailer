using Mailer.Core.Domain.Emails;
using Mailer.Core.Domain.Folders;
using Mailer.Web.Components;
using Mailer.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mailer.Web.Controllers
{
    [Authorize]
    public class FolderController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public FolderController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(FolderType folderId, string targetUpdate)
        {
            return ViewComponent(nameof(FolderMessages), new { folderId = folderId, targetUpdate = targetUpdate });
        }

        [HttpPost]
        public IActionResult Search(FolderMessagesViewModel model, FolderType folderId, string targetUpdate)
        {
            return ViewComponent(nameof(FolderMessages), new
            {
                folderId = folderId,
                emailPriority = model.EmailPriority,
                searchTerm = model.SearchTerm,
                targetUpdate = targetUpdate,
                page = 1 //reset to first page
            });
        }

        [HttpGet]
        public IActionResult Paginate(FolderType folderId,
            string targetUpdate,
            EmailPriority? emailPriority = null,
            string searchTerm = null,
            int page = 1)
        {
            return ViewComponent(nameof(FolderMessages), new
            {
                folderId = folderId,
                emailPriority = emailPriority,
                searchTerm = searchTerm,
                targetUpdate = targetUpdate,
                page = page
            });
        }
    }
}