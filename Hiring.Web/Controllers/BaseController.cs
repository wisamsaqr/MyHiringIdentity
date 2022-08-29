using Hiring.Data.Enums;
using Hiring.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hiring.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected UserType userType;
        protected string UserId;
        private readonly IUserService userService;

        public BaseController(IUserService userService)
        {
            this.userService = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if(User.Identity.IsAuthenticated)
            {
                var user = userService.GetByUsername(User.Identity.Name);
                userType = user.UserType;
                UserId = user.Id;

                ViewBag.UserName = user.UserName;
                ViewBag.Email = user.Email;
                //ViewBag.ImageUrl = user.ImageUrl;
                ViewBag.UserType = user.UserType.ToString();



                //if(user.UserType == UserType.Admin)
                //{

                //    ViewBag.FullName = user.FullName;
                //}
            }
        }
    }
}