
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCorePlayground.TagHelpers
{
    public class MyFirst : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // get the existing content
            var childContent = await output.GetChildContentAsync();
            var innerHtml = childContent.GetContent();

            // wrap all the PreContent, Content, and PostContent in a paragraph tag.
            output.TagName = "p";
            output.TagMode = TagMode.StartTagAndEndTag; // optional

            ModifyTagHelperContent(output);
        }

        public void ModifyTagHelperContent(TagHelperOutput output)
        {
            // outside of the TagName tag
            output.PreElement.SetHtmlContent("<p>PreElement.SetHtmlContent</p>");
            output.PostElement.SetHtmlContent("<p>PostElement.SetHtmlContent</p>");

            // inside the TagName tag
            output.PreContent.SetHtmlContent("<i>PreContent.SetHtmlContent</i>, ");
            output.PostContent.SetHtmlContent("<i>PostContent.SetHtmlContent</i>.");
            output.Content.SetContent("Content.SetContent, "); // replaces existing content
            output.Content.Append("Content.Append, "); 
            output.Content.AppendHtml("<i>Content.AppendHtml</i>, ");
        }
    }
}