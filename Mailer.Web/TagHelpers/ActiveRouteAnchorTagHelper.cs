using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
namespace Mailer.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = "asp-controllers")]
    public class ActiveRouteAnchorTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-controllers")]
        public string Controllers { get; set; }

        [HtmlAttributeName("asp-class")]
        public string Class { get; set; } = "active";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (ShouldBeActive())
            {
                MakeActive(output);
            }

            output.Attributes.RemoveAll("asp-controllers");
        }
        private bool ShouldBeActive()
        {
            string currentController = ViewContext.RouteData.Values["Controller"]?.ToString()?.ToLower();

            var containsActiveController = Controllers?.Split(',')?.FirstOrDefault(x => x.ToLower() == currentController);

            return containsActiveController != null;
        }

        private void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", Class);
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf(Class) < 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value == null
                    ? Class
                    : $"{classAttr.Value} {Class}");
            }
        }
    }
}
