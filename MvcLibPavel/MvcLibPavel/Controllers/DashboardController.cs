using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MvcLibPavel.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            return View();
        }
    }
}
