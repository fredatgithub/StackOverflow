using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddConsole();
            logger.AddDebug();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            var log = logger.CreateLogger("BigFont");

            // Custom middleware for Angular UI-Router
            app.Use(async (context, next) =>
            {
                if (!Path.HasExtension(context.Request.Path.Value)
                    && context.Request.HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest"
                    && context.Request.Method.ToUpper() != "POST"
                    && context.Request.Method.ToUpper() != "PUT"
                    && context.Request.Method.ToUpper() != "DELETE")
                {
                    await context.Response.WriteAsync(File.ReadAllText(env.WebRootPath + "/index.html"));
                }

                await next();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
