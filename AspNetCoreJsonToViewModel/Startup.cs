using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreJsonToViewModel
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }

    public class Student {
        public int StudentId {get;set;}
        public string Firstname {get;set;}
        public string Lastname {get;set;}
    }

    public class Address {
        public int AddressId {get;set;}
        public string Street {get;set;}
    }

    public class StudentController : Controller
    {
        [HttpPost]
        public int Create(IFormCollection studentInfo){
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(studentInfo));
                return 0;
        }
    }
}
