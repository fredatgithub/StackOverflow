using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataLayer;

namespace DataLayer.Migrations
{
    public class Program
    {
        private readonly IConfigurationRoot _configuration;

        public Program()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .Build();
        }

        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Program>()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Step One: Add migration (only necessary if you've changed the schema)
            // dotnet ef migrations add initial --context DataLayer.MyContext

            var currentAssembly = "DataLayer.Migrations";
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            Console.WriteLine($"ConnectionString: {connectionString}");

            services.AddDbContext<MyContext>(optionsBuilder =>
            {
                Console.WriteLine($"Updating the database.");
                optionsBuilder.UseSqlite(
                    connectionString,
                    builder => builder.MigrationsAssembly(currentAssembly));
            });
        }

        public void Configure(MyContext context)
        {
            // Step Two: Update the database (i.e. publish the migrations)
            // dotnet ef database update --environment Development
            // Note: Entity Framework migrations ignore the ASPNETCORE_ENVIRONMENT variable.

            // Step Three: Run the quick demo (remember to update the database first)
            // PS> $env:ASPNETCORE_ENVIRONMENT="Development"
            // PS> dotnet run

            var data = new MyDataModel
            {
                Id = System.Guid.NewGuid()
            };

            context.Add(data);
            context.SaveChanges();

            var count = context.MyDataModels.CountAsync().Result;
            Console.WriteLine($"There are {count} items.");
        }
    }
}
