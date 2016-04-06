using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Content.Upload.Multipart
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (!IsMultipartContentType(context.Request.ContentType))
                {
                    await next();
                    return;
                }

                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html><body>Multipart received<br>");

                // Read the request body as multipart sections. 
                // This does not buffer the content of each section. 
                // If you want to buffer the data
                // then that needs to be added either to the request body before you start or to the individual segments afterward.
                var boundary = GetBoundary(context.Request.ContentType);
                var reader = new MultipartReader(boundary, context.Request.Body);

                var section = await reader.ReadNextSectionAsync();
                while (section != null)
                {
                    await context.Response.WriteAsync("- Header count: " + section.Headers.Count + "<br>");
                    foreach (var headerPair in section.Headers)
                    {
                        await context.Response.WriteAsync("-- " + headerPair.Key + ": " + string.Join(", ", headerPair.Value) + "<br>");
                    }

                    // Consume the section body here.

                    // Nested - see original sample.

                    // Drains any remaining section body that has not been consumed and reads the headers for the next section.
                    section = await reader.ReadNextSectionAsync();
                }

                await context.Response.WriteAsync("</body></html>");
            });

            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html><body>");

                await context.Response.WriteAsync(@"
<FORM action = ""/"" method=""post"" enctype=""multipart/form-data"" >
<LABEL for=""myfile1"">File 1:</LABEL>
<INPUT type=""file"" name=""myfile1"" /><BR>
<LABEL for=""myfile2"">File 2:</LABEL>
<INPUT type=""file"" name=""myfile2"" /><BR>
<INPUT type=""submit"" value=""Send"" />
</FORM>");

                await context.Response.WriteAsync("</body></html>");
            });
        }

        private static bool IsMultipartContentType(string contentType)
        {
            return !string.IsNullOrEmpty(contentType) && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string GetBoundary(string contentType)
        {
            // TODO: Strongly typed headers will take care of this for us
            // TODO: Limit the length of boundary we accept. The spec says ~70 chars.
            var elements = contentType.Split(' ');
            var element = elements.Where(entry => entry.StartsWith("boundary=")).First();
            var boundary = element.Substring("boundary=".Length);
            // Remove quotes
            if (boundary.Length >= 2 && boundary[0] == '"' && boundary[boundary.Length - 1] == '"')
            {
                boundary = boundary.Substring(1, boundary.Length - 2);
            }
            return boundary;
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseDefaultHostingConfiguration(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}

