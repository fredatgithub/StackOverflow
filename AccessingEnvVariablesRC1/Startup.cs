using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;

namespace AccessingEnvVariablesRC1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddJsonFile("config.json");
            }

            builder.AddEnvironmentVariables();

            mConfiguration = builder.Build();
        }

        public IConfigurationRoot mConfiguration { get; set; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(env.EnvironmentName);
                await context.Response.WriteAsync($"\r\n");

                await context.Response.WriteAsync(mConfiguration["bar"]);
                await context.Response.WriteAsync($"\r\n");
            });
        }

        public static void Main(string[] args) =>
            Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
