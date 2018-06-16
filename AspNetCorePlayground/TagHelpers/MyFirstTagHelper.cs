
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCorePlayground.TagHelpers
{
    public class MyFirst : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var innerHtml = childContent.GetContent();

            output.Content.SetContent("Content.SetContent, "); // [replaces existing content]
            output.Content.Append("Content.Append, "); // [append new content]
            output.Content.AppendFormat("Content.AppendFormat {0} {1} {2}, ", "Foo", "Bar", "Baz");
            output.Content.AppendHtml("<strong>Content.AppendHtml</strong>.");

            output.PreContent.SetHtmlContent("<p>PreContent.SetHtmlContent</p>");
            output.PostContent.SetHtmlContent("<p>PostContent.SetHtmlContent</p>");
            output.PreElement.SetHtmlContent("<p>PreElement.SetHtmlContent</p>");
            output.PostElement.SetHtmlContent("<p>PostElement.SetHtmlContent</p>");
        }
    }
}