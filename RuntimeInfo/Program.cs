using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Program
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var message = string.Format(
                    "System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription: {0}", System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
                
                return context.Response.WriteAsync(message);
            });
        }
        
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                        .UseServer("Microsoft.AspNetCore.Server.Kestrel")
                        .UseIISPlatformHandlerUrl()
                        .UseStartup<Program>()
                        .Build();

            host.Run();
        }
    }
}
