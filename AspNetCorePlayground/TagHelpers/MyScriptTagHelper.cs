using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCorePlayground.TagHelpers
{
    [HtmlTargetElement("script")]
    public class MyScriptTagHelper : ScriptTagHelper
    {
        public MyScriptTagHelper(
            IHostingEnvironment hostingEnvironment, 
            IMemoryCache cache, 
            HtmlEncoder htmlEncoder, 
            JavaScriptEncoder javaScriptEncoder, 
            IUrlHelperFactory urlHelperFactory) : base(
                hostingEnvironment, 
                cache, 
                htmlEncoder, 
                javaScriptEncoder, 
                urlHelperFactory)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AppendVersion = true;
            base.Process(context, output);
        }
    }

}