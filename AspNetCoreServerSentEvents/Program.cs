using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ConsoleApplication
{
    public class Program
    {
        object Task;

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.ToString();
                if (path.Contains("sse"))
                {
                    context.Response.Headers.Add("Content-Type", "text/event-stream");
                }

                await System.Threading.Tasks.Task.Delay(1);
                return;
            });
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Program>()
                .Build();

            host.Run();
        }
    }
}
