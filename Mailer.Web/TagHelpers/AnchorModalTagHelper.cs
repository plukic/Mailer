using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mailer.Web.TagHelpers
{
    /// <summary>
    /// Appends necessary modal data attributes to selected element
    /// </summary>
    [HtmlTargetElement(Attributes = "modal-toggle")]
    public class AnchorModalTagHelper : TagHelper
    {
        [HtmlAttributeName("modal-toggle")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Target modal identifier
        /// </summary>
        [HtmlAttributeName("modal-id")]
        public string TargetId { get; set; } = "#modal-container";

        [HtmlAttributeName("modal-size")]
        public ModalSize ModalSize { get; set; } = ModalSize.Md;

        [HtmlAttributeName("modal-backdrop")]
        public ModalBackdrop ModalBackdrop { get; set; } = ModalBackdrop.Static;

        [HtmlAttributeName("modal-position")]
        public ModalPosition ModalPosition { get; set; } = ModalPosition.Default;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            if (Enabled)
            {
                output.Attributes.Add("data-toggle", "modal");
                output.Attributes.Add("data-target", TargetId);
                output.Attributes.Add("data-backdrop", ModalBackdrop.ToModalBackdropClass());
                output.Attributes.Add("data-modal-size", ModalSize.ToModalSizeClass());
                output.Attributes.Add("data-modal-position", ModalPosition.ToModalPositionClass());
            }
        }
    }

    public enum ModalSize
    {
        Sm = 1,
        Md = 2,
        Lg = 3,
        Xl = 4
    }

    public enum ModalBackdrop
    {
        None = 1,
        Static = 2
    }

    /// <summary>
    /// Extension methods for <see cref="ModalSize"/> used only inside <see cref="AnchorModalTagHelper"/>
    /// </summary>
    internal static class ModalSizeExtensions
    {
        /// <summary>
        /// Returns proper insertion value based on enum <paramref name="value"/>
        /// </summary>
        /// <param name="value">Insertion mode</param>
        /// <returns></returns>
        public static string ToModalSizeClass(this ModalSize value)
        {
            return value switch
            {
                ModalSize.Sm => "sm",
                ModalSize.Md => "md",
                ModalSize.Lg => "lg",
                ModalSize.Xl => "xl",
                _ => "md",
            };
        }
    }

    internal static class ModalBackdropExtensions
    {
        /// <summary>
        /// Returns proper insertion value based on enum <paramref name="value"/>
        /// </summary>
        /// <param name="value">Insertion mode</param>
        /// <returns></returns>
        public static string ToModalBackdropClass(this ModalBackdrop value)
        {
            return value switch
            {
                ModalBackdrop.None => "none",
                ModalBackdrop.Static => "static",
                _ => "static",
            };
        }
    }

    public enum ModalPosition
    {
        Top = 1,
        Center = 2,
        Bottom = 3,
        Aside = 4,
        Default = 5 // This is a default case with only "modal-position" class
    }

    /// <summary>
    /// Extension methods for <see cref="ModalPosition"/> used only inside <see cref="AnchorModalTagHelper"/>
    /// </summary>
    internal static class ModalPositionExtensions
    {
        /// <summary>
        /// Returns proper insertion value based on enum <paramref name="value"/>
        /// </summary>
        /// <param name="value">Insertion mode</param>
        /// <returns></returns>
        public static string ToModalPositionClass(this ModalPosition value)
        {
            return value switch
            {
                ModalPosition.Top => "top",
                ModalPosition.Center => "center",
                ModalPosition.Bottom => "bottom",
                ModalPosition.Aside => "aside",
                // No need to append any class for default position
                _ => "top",
            };
        }
    }
}
