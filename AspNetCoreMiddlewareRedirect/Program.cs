using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MyXmlSample
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                // if specific condition does not meet
                if (context.Request.Path.ToString().Equals("/foo"))
                {
                    context.Response.Redirect("http://www.google.com");
                }
            });
            
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
}
