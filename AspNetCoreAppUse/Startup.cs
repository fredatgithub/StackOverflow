using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreAppUse
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Syntax 1.
            // Use(this IApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware);
            app.Use((context, next) =>
            {
                context.Response.WriteAsync("Hello world");
                return next();
            });

            // Syntax 2.
            // Use(this IApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware);
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello world");
                await next();
            });

            // Syntax 3.
            // Use(this IApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware);
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello world");
                await next.Invoke();
            });

            // Syntax 4.
            app.Use(next =>
            {
                return ctx =>
                {
                    ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                    return next(ctx);
                };
            });
        }
    }
}
