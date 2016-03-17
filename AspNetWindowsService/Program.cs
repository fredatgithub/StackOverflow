using System.ServiceProcess;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.AspNet.Http;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace MyProject
{
    public class Program : ServiceBase
    {
        private IApplication _application;
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.Run(async context => {
                await context.Response.WriteAsync(env.WebRootPath);
            });
        }

        public static void Main(string[] args)
        {
            try
            {
                if (args.Contains("--windows-service"))
                {
                    Run(new Program());
                    return;
                }

                var program = new Program();
                program.OnStart(null);
                Console.ReadLine();
                program.OnStop();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected override void OnStart(string[] args)
        {
            var configProvider = new MemoryConfigurationProvider();
            configProvider.Add("server.urls", "http://localhost:5000");
            configProvider.Add("webroot", "./../../../my-root");

            var config = new ConfigurationBuilder()
                .Add(configProvider)
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            _application = new WebHostBuilder(config)
                .UseServer("Microsoft.AspNet.Server.Kestrel")
                .UseStartup<Program>()
                .Build()
                .Start();
        }

        protected override void OnStop()
        {
            _application?.Dispose();
        }
    }
}
