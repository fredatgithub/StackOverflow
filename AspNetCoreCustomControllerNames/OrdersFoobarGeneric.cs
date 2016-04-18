using Microsoft.AspNetCore.Mvc;

namespace CustomControllerNames 
{
    [Route("generic-orders")]
    public class OrdersFoobarController<T> : Controller where T : IOrder
    {
        [HttpGet]
        public ActionResult Get()
        {
            return new OkResult();
        }
    }
    
    public interface IOrder
    {
        
    }
    
    public class SimpleOrder : IOrder
    {
        
    }
}