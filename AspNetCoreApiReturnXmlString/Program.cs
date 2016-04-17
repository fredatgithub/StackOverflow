using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
