using System;
using Microsoft.AspNet.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class SubscribersController : Controller
    {
        public IActionResult Index()
        {
            var subscriber = new
            {
                Id = Guid.NewGuid(),
                FirstName = "Shaun",
                LastName = "Luttin"
            };

            return CreatedAtRoute(
                routeName: "SubscriberLink",
                routeValues: new { id = subscriber.Id },
                value: subscriber);
        }

        [HttpGet("{id}", Name = "SubscriberLink")]
        public IActionResult GetSubscriber(Guid id)
        {
            var subscriber = new
            {
                Id = id,
                FirstName = "Shaun",
                LastName = "Luttin"
            };

            return new JsonResult(subscriber);
        }
    }
}
