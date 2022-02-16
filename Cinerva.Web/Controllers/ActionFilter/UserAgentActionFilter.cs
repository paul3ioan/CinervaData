using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cinerva.Web.Controllers.ActionFilter
{
    public class UserAgentActionFilter : IActionFilter
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserAgentActionFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
                controller.ViewBag.userAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
