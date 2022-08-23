using Microsoft.AspNetCore.Mvc;
using MvcLibPavel.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcLibPavel.Controllers
{
    public class BooksSummaryController : Controller
    {
        public static string baseUrl = "https://localhost:7272/api/BooksDetails";

        public async Task<IActionResult> Index()
        {
            var getBooksSummary = await GetBooksSummary();
            return View(getBooksSummary);
        }
        ////////////
        [HttpGet]
        public async Task<List<BooksSummary>> GetBooksSummary()
        {
            //Use the access token to call a protected web API.
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<List<BooksSummary>>(jsonStr).ToList();

            return res;
        }
    }
}
