using System;
using System.IO;
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

                var boundary = GetBoundary(context.Request.ContentType);
                var reader = new MultipartReader(boundary, context.Request.Body);

                var section = await reader.ReadNextSectionAsync();

                const int chunkSize = 1024;
                var buffer = new byte[chunkSize];

                while (section != null)
                {
                    using (var stream = new FileStream("my-file.jpg", FileMode.Append))
                    {
                        int bytesRead;

                        do
                        {
                            bytesRead = await section.Body.ReadAsync(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, bytesRead);

                        } while (bytesRead > 0);


                    }

                    section = await reader.ReadNextSectionAsync();
                }
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

