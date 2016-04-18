using Microsoft.AspNetCore.Mvc;

namespace CustomControllerNames 
{
    [Route("generic-orders")]
    public class OrdersFoobarController<T>
    {
        [HttpGet]
        public ActionResult Get()
        {
            return new OkResult();
        }
    }
}