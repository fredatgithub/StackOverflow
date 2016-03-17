using System.ServiceProcess;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

public class Startup : ServiceBase
{
    IApplication _application;

    public void Main(string[] args)
    {
        Run(this);
    }

    protected override void OnStart(string[] args)
    {
        var configProvider = new MemoryConfigurationProvider();
        configProvider.Add("server.urls", "http://*:80");

        var config = new ConfigurationBuilder()
            .Add(configProvider)
            .AddEnvironmentVariables()
            .Build();

        _application = new WebHostBuilder(config)
            .UseServer("Microsoft.AspNet.Server.Kestrel")
            .UseStartup<Startup>()
            .Build()
            .Start();
    }

    protected override void OnStop()
    {
    }
}