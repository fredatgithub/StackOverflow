using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment host)
        {
            Console.WriteLine(host.WebRootPath);

            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true
            });

            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.ToString();
                if (path.Contains("sse"))
                {
                    context.Response.Headers.Add("Content-Type", "text/event-stream");
                }

                await Task.Delay(1);
                return;
            });
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                .UseStartup<Program>()
                .Build();

            host.Run();
        }
    }
}
