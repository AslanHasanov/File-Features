
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc.Filters;
using DemoApplication.Areas.Client.Controllers;
using DemoApplication.Controllers;

namespace DemoApplication.Areas.Client.ActionFilter
{
    public class ValidationCurrentUserAttribute : IActionFilter
    {
        private readonly IUserService _userService;

        public ValidationCurrentUserAttribute(IUserService userService)
        {
            _userService = userService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_userService.IsAuthenticated)
            {
                var controller = (AuthenticationController)context.Controller;
                context.Result = controller.RedirectToRoute("client-account-dashboard");
            }
        }
    }
}
