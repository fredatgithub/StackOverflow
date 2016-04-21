using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MiddlewareRedirectSample
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
        }

        public void Configure(IApplicationBuilder app)
        {
            // inline middleware
            app.Use(async (context, next) =>
            {
                // if specific condition does not meet
                if (context.Request.Path.ToString().Equals("/foo"))
                {
                    context.Response.Redirect("http://www.google.com");
                }
                else
                {
                    await next.Invoke();
                }
            });

            // middleware class
            app.UseMiddleware<RedirectMiddleware>();

            app.UseMvc();
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Program>()
                .Build();

            host.Run();
        }
    }

    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // if specific condition does not meet
            if (context.Request.Path.ToString().Equals("/bar"))
            {
                context.Response.Redirect("http://www.google.com");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
