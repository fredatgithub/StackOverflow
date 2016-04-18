using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CustomControllerNames
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .ConfigureApplicationPartManager(manager => 
                {
                    manager.FeatureProviders.Add(new MyControllerFeatureProvider());
                });
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel:LogLevel.Debug);
            
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
