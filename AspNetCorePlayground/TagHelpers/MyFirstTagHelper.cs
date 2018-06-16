using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCorePlayground.TagHelpers
{
    public class MyFirst : TagHelper
    {
        public string MyStringProperty { get; set; }

        public DateTime MyDateTimeProperty { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            ModifyTag(output);
            ModifyAttributes(output);
            ModifyContents(output);

            // get the markup content from the .cshtml file
            var childContent = await output.GetChildContentAsync();
            var innerHtml = childContent.GetContent();

            output.Content.AppendHtml($"<li>{innerHtml}");
            output.Content.AppendHtml($"<li><mark>{MyStringProperty}</mark>");
            output.Content.AppendHtml($"<li><mark>{MyDateTimeProperty.ToString("D")}</mark>");
        }

        public void ModifyTag(TagHelperOutput output)
        {
            // wrap all the PreContent, Content, and PostContent in a paragraph tag.
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
            output.PreElement.SetHtmlContent("<p>PreElement.SetHtmlContent");
            output.PostElement.SetHtmlContent("<p>PostElement.SetHtmlContent");

            // inside the TagName tag
            output.PreContent.SetHtmlContent("<li>PreContent.SetHtmlContent");
            output.PostContent.SetHtmlContent("<li>PostContent.SetHtmlContent");

            output.Content
                .SetHtmlContent("<li>Content.SetHtmlContent") // replaces existing content
                .AppendHtml("<li>Content.AppendHtml");
        }
    }
}