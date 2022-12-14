using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcLibPavel.Configs;
using MvcLibPavel.Constants;
using MvcLibPavel.Controllers.Interfaces;
using MvcLibPavel.Models;
using Newtonsoft.Json;
using Polly;
using RestEase;
using System.Net.Http.Headers;

namespace MvcLibPavel.Controllers
{
    public class BooksSummaryController : Controller
    {
        private readonly Settings _settings;
        public BooksSummaryController()
        {
            _settings = new Configs.Configs().GetConfig();
        }

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
            List<BooksSummary> res = new List<BooksSummary>();
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .RetryAsync(LibConstants.MaxRetryAttempts);
           
                await retryPolicy.ExecuteAsync(async () =>
                {
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    var url = _settings.BooksDetailsPath;
                    IBooksSummaryController api = RestClient.For<IBooksSummaryController>(url);
                    api.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    try
                    {
                        var json = await api.GetAsync();
                        res = JsonConvert.DeserializeObject<List<BooksSummary>>(json).ToList();
                    }
                    catch
                    {
                        throw;
                    }
                });
            return res;
        }
    }
}
