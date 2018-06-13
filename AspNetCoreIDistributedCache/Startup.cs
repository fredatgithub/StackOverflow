using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

// required for IDistributedCache
using Microsoft.Extensions.Caching.Distributed;

// required for Encoding
using System.Text;

// See StackOverflow question here: 
// https://stackoverflow.com/questions/50822279/multiple-instances-of-idistributedcache
namespace AspNetCoreIDistributedCache
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
                // dotnet add package Microsoft.Extensions.Caching.Redis
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = "localhost";
                    options.InstanceName = "SampleInstance";
                });
        }

        // Install and start Redis locally:
        // 
        // PS> Set-ExecutionPolicy Bypass -Scope Process -Force; 
        // PS> iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
        // PS> choco install redis-64
        // PS> redis-server
        // 
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            IDistributedCache cache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var bytes = await cache.GetAsync("lastRequestTime");
                var last = bytes == null 
                    ? "There has not been a request recently."
                    : Encoding.UTF8.GetString(bytes);

                var now = DateTime.Now.ToString();
                var val = Encoding.UTF8.GetBytes(now);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));

                cache.Set("lastRequestTime", val, options);

                await context.Response.WriteAsync($"Hello World! {last}");
            });
        }
    }
}
