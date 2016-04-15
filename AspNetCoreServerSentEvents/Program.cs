using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Program
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context => {
               return context.Response.WriteAsync($"Hello world!"); 
            }); 
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Program>()
                .Build();

            host.Run();
        }
    }
}
