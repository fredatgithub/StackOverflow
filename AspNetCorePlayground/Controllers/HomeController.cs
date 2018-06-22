using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCorePlayground.Models;
using AspNetCorePlayground.Filters;

namespace AspNetCorePlayground.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [TypeFilter(typeof(MyActionFilter))]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

    [HttpGet("test")]
    public IActionResult TestMyFirstModelBinder(MyFirstModelBinderTest model)
    {
        return Json(model);
    }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
