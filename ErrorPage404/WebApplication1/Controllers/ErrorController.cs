namespace WebApplication1.Controllers
{
    using Microsoft.AspNet.Diagnostics;
    using Microsoft.AspNet.Http.Features;
    using Microsoft.AspNet.Mvc;

    [Route("[controller]")]
    public class ErrorController : Controller
    {
        [HttpGet("{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (feature == null || feature.Error == null)
            {
                var obj = new { message = "Hey. What are you doing here?"};
                return new HttpNotFoundObjectResult(obj);
            }

            return View("Error", statusCode);
        }
    }
}
