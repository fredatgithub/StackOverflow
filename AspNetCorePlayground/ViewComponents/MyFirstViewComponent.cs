

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePlayground.ViewComponents
{
    public class MyFirstViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() {
            await Task.Delay(0);
            return View();
        }
    }
}