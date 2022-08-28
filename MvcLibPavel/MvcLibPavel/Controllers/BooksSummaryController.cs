using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcLibPavel.Models;
using Newtonsoft.Json;
using Polly;
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
            var httpClient = new HttpClient();
            var maxRetryAttempts = 3;
            List<BooksSummary> res = new List<BooksSummary>();
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .RetryAsync(maxRetryAttempts);
           
                await retryPolicy.ExecuteAsync(async () =>
                {
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    var url = baseUrl;
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var response = await httpClient.GetAsync(url);

                    try
                    {
                        response.EnsureSuccessStatusCode();
                    }
                    catch (HttpRequestException e)
                    {
                        throw;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<List<BooksSummary>>(json).ToList();
                });
            return res;
        }
    }
}
