using Mailer.Core.Domain.Emails;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Folders;
using Mailer.Core.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace Mailer.Web.Components
{
    public class FolderMessagesViewModel
    {

        public string TargetUpdate { get; set; }
        public FolderType FolderId { get; set; }
        public string FolderName { get; set; }


        #region Search fields
        [Display(Prompt =LocalizationKeys.SearchByEmail)]
        public string SearchTerm { get; set; }
        public EmailPriority? EmailPriority { get; set; }
        public List<SelectListItem> EmailPriorities { get; set; }
        #endregion


        #region Search result
        public StaticPagedList<EmailDto> Emails { get; set; }
        #endregion

    }
}
