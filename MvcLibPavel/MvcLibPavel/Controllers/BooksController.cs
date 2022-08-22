using Microsoft.AspNetCore.Mvc;
using MvcLibPavel.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcLibPavel.Controllers
{
    public class BooksController : Controller
    {
        public static string baseUrl = "https://localhost:7272/api/Books/";
        //public static string baseUrl = "https://localhost:7272/api/Author/";
        public async Task<IActionResult> Index()
        {
            var books = await GetBooks();
            return View(books);
        }

        [HttpGet]
        public async Task<List<Books>> GetBooks() 
        {
            //Use the access token to call a protected web API.
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<List<Books>>(jsonStr).ToList();

            return res; 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Title, BookYear, AuthorId")] Books books)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(books), Encoding.UTF8, "application/json");
            await client.PostAsync(url, stringContent);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<Books>(jsonStr);

            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);
            var books = JsonConvert.DeserializeObject<Books>(jsonStr);

            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

    }
}
