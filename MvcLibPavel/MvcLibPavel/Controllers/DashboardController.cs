using Microsoft.AspNetCore.Mvc;

namespace MvcLibPavel.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            // check on null or somethng

            //if not null - redirect
            

            return View();
        }
    }
}
