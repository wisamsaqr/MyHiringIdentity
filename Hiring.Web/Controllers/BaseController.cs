using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hiring.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}