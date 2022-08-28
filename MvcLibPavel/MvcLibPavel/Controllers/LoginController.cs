using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcLibPavel.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace MvcLibPavel.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginUser(UserInfo user)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                var retriesLeft = 10;
                while (retriesLeft > 0)
                {
                    try
                    {
                        var response = await httpClient.PostAsync("https://localhost:7272/Auth/login?data", stringContent);
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            break;
                        }
                        using (response)
                        {
                            var token = await response.Content.ReadAsStringAsync();
                            if (!response.IsSuccessStatusCode)
                            {
                                ViewBag.ErrorMessage= "Incorrect UserId or Password!";
                                return View("Index");
                            }
                            HttpContext.Session.SetString("JWToken", token);
                            break;
                        }
                    }
                    catch
                    {
                        retriesLeft--;
                        if (retriesLeft == 0)
                        {
                            throw;
                        }
                    }
                }
            }
            return Redirect("~/BooksSummary/Index");
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();

            return Redirect("~/Login/Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}