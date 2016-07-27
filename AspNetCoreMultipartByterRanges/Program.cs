using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        var host = new WebHostBuilder()
            .ConfigureLogging(options => options.AddConsole())
            .ConfigureLogging(options => options.AddDebug())
            .UseKestrel()
            .UseStartup<Startup>()
            .Build();

        host.Run();
    }
}
