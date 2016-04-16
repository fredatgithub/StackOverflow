using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServerSentEventSample
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvcCore();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment host)
        {
            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true
            });

            // middleware for server socket events
            // this works on its own without the need for MVC.
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.ToString().Equals("/sse"))
                {
                    var response = context.Response;

                    response.Headers.Add("Content-Type", "text/event-stream");

                    await response.WriteAsync($"data: First message...\r\r");

                    var i = 0;

                    while (true)
                    {
                        await Task.Delay(5 * 1000);

                        await response
                            .WriteAsync($"data: Middleware #{i++} at {DateTime.Now}\r\r");
                    }
                }

                await next.Invoke();
            });

            app.UseDeveloperExceptionPage();

            app.UseMvc();
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
