
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AspNetCorePlayground
{
    public class CustomUrlHelper : UrlHelper
    {
        public CustomUrlHelper(ActionContext actionContext)
            : base(actionContext) { }

        public override string Action(UrlActionContext actionContext)
        {
            var controller = actionContext.Controller;
            var action = actionContext.Action;
            return $"You wrote {controller} > {action}!";
        }
    }

    public class CustomUrlHelperFactory : IUrlHelperFactory
    {
        public IUrlHelper GetUrlHelper(ActionContext context)
        {
            return new CustomUrlHelper(context);
        }
    }
}