using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Program
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var path = context.Request.Path.ToString();
                if (path.Contains("sse"))
                {
                    context.Response.Headers.Add("Content-Type", "text/event-stream");
                }

                return context.Response.WriteAsync($"{path}");
            });
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Program>()
                .Build();

            host.Run();
        }
    }
}
