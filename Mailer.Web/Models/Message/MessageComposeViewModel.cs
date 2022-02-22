using Mailer.Core.Domain.Emails;
using Mailer.Core.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Mailer.Web.Models.Message
{
    public class MessageComposeViewModel
    {
        public int? Id { get; set; }
        [Display(Name = LocalizationKeys.To)]
        public List<string> To { get; set; }
        [Display(Name = LocalizationKeys.Cc)]
        public List<string> Cc { get; set; }
        [Display(Name = LocalizationKeys.Bcc)]
        public List<string> Bcc { get; set; }
        [Display(Name = LocalizationKeys.Subject, Prompt = LocalizationKeys.AddASubject)]
        public string Subject { get; set; }
        public string Body { get; set; }
        [Display(Name = LocalizationKeys.EmailPriority)]
        public EmailPriority EmailPriority { get; set; }
        public IList<SelectListItem> EmailPriorities { get; set; }

        public string TargetUpdate { get; set; }

        public MessageComposeViewModel()
        {
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
        }
        public List<SelectListItem> ToSelectList(List<string> inputList)
        {
            if (inputList == null)
                return new List<SelectListItem>();

            return inputList.Select(x => new SelectListItem
            {
                Text = x,
                Value = x
            }).ToList();
        }
    }
}
