using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

    public class FooBarViewComponent : ViewComponent {
        public async Task<IViewComponentResult> InvokeAsync() {
            throw new System.Exception("Exception in FooBar");
            await Task.Delay(0);
            return View();
        }
    }