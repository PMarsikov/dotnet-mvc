using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var getBooksSummary = await GetBooksSummary();
                return View(getBooksSummary);
            }
            catch (HttpRequestException e)
            {
                return Redirect("~/Login/Index");
            }

            catch (Exception e)
            {
                return Redirect("~/DashboarError/Index");
            }
        }

        [HttpGet]
        public async Task<List<BooksSummary>> GetBooksSummary()
        {
            //Use the access token to call a protected web API.
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync(url);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw;
            }

            var json = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<List<BooksSummary>>(json).ToList();

            return res;
        }
    }
}
