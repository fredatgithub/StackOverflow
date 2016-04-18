using Microsoft.AspNetCore.Mvc;

namespace CustomControllerNames 
{
    [Route("orders")]
    public class OrdersFoobar
    {
        [HttpGet]
        public ActionResult Get()
        {
            return new OkResult();
        }
    }
}