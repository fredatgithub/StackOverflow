
namespace WebApplication1.Controllers
{
    using Microsoft.AspNet.Mvc;

    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            throw new System.Exception();
        }
    }
}
