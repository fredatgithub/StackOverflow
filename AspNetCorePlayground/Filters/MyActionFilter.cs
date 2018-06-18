using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCorePlayground.Filters
{
public class MyActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.Result = new BadRequestObjectResult(new {Error = "JWT Token is expired."});
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        System.Console.WriteLine("Bar");
    }
}
}