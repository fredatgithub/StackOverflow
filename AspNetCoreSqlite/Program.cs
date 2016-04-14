using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello World!");
            });
        }

        public static void Main(string[] args)
        {
            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "wwwroot");
            var dataRoot = Path.Combine(contentRoot, "appdata");

            Console.WriteLine("---");
            Console.WriteLine($"contentRoot:{contentRoot}");
            Console.WriteLine($"webRoot:{webRoot}");
            Console.WriteLine($"dataRoot:{dataRoot}");
            Console.WriteLine("---");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseIISPlatformHandlerUrl()
            //     .UseIISIntegration()
            //     .UseContentRoot(Directory.GetCurrentDirectory())
            //     .UseDefaultHostingConfiguration(args)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
