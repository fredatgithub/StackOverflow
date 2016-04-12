namespace WebApplication1
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseMvc();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
