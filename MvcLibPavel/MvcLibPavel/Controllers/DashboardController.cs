using Microsoft.AspNetCore.Mvc;

namespace MvcLibPavel.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
