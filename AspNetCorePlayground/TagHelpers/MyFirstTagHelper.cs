
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

            output.Content.Append(innerHtml);
        }
    }
}