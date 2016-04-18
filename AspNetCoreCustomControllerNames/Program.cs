using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Internal;

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
