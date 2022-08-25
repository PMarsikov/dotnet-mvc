using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MvcLibPavel.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            //var aa = HttpContext.User;
            // check on null or somethng

            //if not null - redirect
            //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            //var user = await _userManager.FindByEmailAsync(email);

            return View();
        }
    }
}
