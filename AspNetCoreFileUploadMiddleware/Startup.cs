using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Content.Upload.Files
{
    public class Startup
    {
        private string GetFileName(IFormFile file)
        {
            return file.ContentDisposition
                            .Split(';')
                            .SingleOrDefault(part => part.Contains("filename"))
                            .Split('=')
                            .Last()
                            .Trim('"');
        }

        private async void Download(IFormFile file, string fileName)
        {
            var bufferSize = 1024;
            using (var fileStream = File.Create(fileName, bufferSize, FileOptions.Asynchronous))
            {
                var inputStream = file.OpenReadStream();
                await inputStream.CopyToAsync(fileStream, bufferSize);
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html><body>");

                if (context.Request.HasFormContentType)
                {
                    /*
                    var form = await context.Request.Body.Read();

                    await context.Response.WriteAsync("Files received: " + form.Files.Count + " entries.<br>");

                    foreach (var file in form.Files)
                    {
                        var fileName = GetFileName(file);
                        // Download(file, fileName);

                        await context.Response.WriteAsync("- Content-Disposition: " + file.ContentDisposition + "<br>");
                        await context.Response.WriteAsync("- Length: " + file.Length + "<br>");
                        await context.Response.WriteAsync("- FileName: " + fileName + "<br>");
                    }
                    */
                }

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

