using Microsoft.AspNetCore.Mvc;
using MvcLibPavel.Configs;
using MvcLibPavel.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcLibPavel.Controllers
{
    public class RegisterController : Controller
    {
        private readonly Settings _settings;
        public RegisterController()
        {
            _settings = new Configs.Configs().GetConfig();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RegisterUser(UserInfo user)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_settings.RegisterPath, stringContent);
            }
            return Redirect("~/Login/Index");
        }
    }
}