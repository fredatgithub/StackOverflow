using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCorePlayground.TagHelpers
{
    public class MyFirstTagHelper : TagHelper
    {
        public string MyStringProperty { get; set; }

        public DateTime MyDateTimeProperty { get; set; }

        public override async Task ProcessAsync(
            TagHelperContext context,
            TagHelperOutput output)
        {
            ModifyTag(output);
            ModifyAttributes(output);
            ModifyContents(output);

            // get the markup content from the .cshtml file
            var childContent = await output.GetChildContentAsync();
            var innerHtml = childContent.GetContent();

            output.Content
                .AppendHtml($"<li>{innerHtml}")
                .AppendHtml($"<li>{MyStringProperty}")
                .AppendHtml($"<li>{MyDateTimeProperty.ToString("D")}");
        }

        public void ModifyTag(TagHelperOutput output)
        {
            // wrap the PreContent, Content, and PostContent in a list.
            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag; // optional
        }

        public void ModifyAttributes(TagHelperOutput output)
        {
            output.Attributes.Add("class", "small text-primary");
        }

        public void ModifyContents(TagHelperOutput output)
        {
            // outside of the TagName tag
            output.PreElement
                .SetHtmlContent("<p>PreElement.SetHtmlContent(string)");
            output.PostElement
                .SetHtmlContent("<p>PostElement.SetHtmlContent(string)");

            // inside the TagName tag
            output.PreContent
                .SetHtmlContent("<li>PreContent.SetHtmlContent(string)");
            output.PostContent
                .SetHtmlContent("<li>PostContent.SetHtmlContent(string)");
            output.Content
                // Set... replaces existing content
                .SetHtmlContent("<li>Content.SetHtmlContent(string)")
                .AppendHtml("<li>Content.AppendHtml(string)");
        }
    }
}