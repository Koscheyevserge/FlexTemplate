using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexTemplate.PresentationLayer.Core
{
    [HtmlTargetElement("a", Attributes = PushKeyAttributeName)]
    public class PushKeyTagHelper : TagHelper
    {
        private const string PushKeyAttributeName = "push-key";
        private const string PushValueAttributeName = "push-value";
        
        [HtmlAttributeName(PushKeyAttributeName)]
        public string PushKey { get; set; }

        [HtmlAttributeName(PushValueAttributeName)]
        public string PushValue { get; set; } = string.Empty;
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var href = output.Attributes["href"].Value.ToString();
            var hrefDiff = $"{PushKey}={PushValue}";
            if (href.Contains("?"))
            {
                if (href.Contains(hrefDiff))
                {
                    href = href.Replace(hrefDiff, string.Empty);

                }
                else
                {
                    href += hrefDiff;
                }
            }
            else
            {
                href += "?"; 
                href += hrefDiff;
            }
            output.Attributes.SetAttribute("href", href);
            base.Process(context, output);
        }
    }
}
