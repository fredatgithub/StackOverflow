using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;

        public Startup(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddEntityFramework()
                .AddEntityFrameworkInMemoryDatabase()
                .AddEntityFrameworkSqlite()
                .AddDbContext<AlgaeDbContext>(options =>
                {
                    var basePath = _hostingEnv.ContentRootPath;
                    var dbName = "Algae.db";
                    var dbPath = Path.Combine(basePath, dbName);
                    options.UseSqlite($"Filename={dbPath}");
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                context.Response.WriteAsync(_hostingEnv.ContentRootPath + "\r\n");
                context.Response.WriteAsync(_hostingEnv.WebRootPath + "\r\n");

                return context.Response.WriteAsync("Hello World!");
            });
        }

        public static void Main(string[] args)
        {
            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "wwwroot");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseIISPlatformHandlerUrl()
                .UseContentRoot(contentRoot)
                .UseWebRoot(webRoot)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class AlgaeDbContext : DbContext
    {
        public DbSet<Sandbox> Sandboxes { get; set; }

        public AlgaeDbContext()
        { }

        public AlgaeDbContext(DbContextOptions<AlgaeDbContext> options) : base(options)
        {
        }
    }

    public class Sandbox
    {
        public int SandboxId { get; set; }
    }
}
