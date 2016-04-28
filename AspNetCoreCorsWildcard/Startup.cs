using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNet.Http;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions();
        
        services.TryAdd(
            ServiceDescriptor.Transient<ICorsService, MyCorsService>());
            
        services.TryAdd(
            ServiceDescriptor.Transient<ICorsPolicyProvider, DefaultCorsPolicyProvider>());
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole(minLevel: LogLevel.Verbose);
        
        app.UseCors(corsPolictyBuilder =>
        {
            corsPolictyBuilder.WithOrigins("*.mydomain.com");
        });

        app.Run(async context =>
        {
            await context.Response.WriteAsync(
                $"Is Cors? {context.Request.Headers.ContainsKey(CorsConstants.Origin)}");
        });
    }

    public static void Main(string[] args) =>
        Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
}