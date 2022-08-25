using Microsoft.AspNetCore.Mvc;
using MvcLibPavel.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcLibPavel.Controllers
{
    public class AuthorsController : Controller
    {
        public static string baseUrl = "https://localhost:7272/api/Author/";
   
        public async Task<IActionResult> Index()
        {
            var authors = await GetAuthors();
            return View(authors);
        }
        ////////////
        [HttpGet]
        public async Task<List<Author>> GetAuthors()
        {
            //Use the access token to call a protected web API.
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<List<Author>>(jsonStr).ToList();

            return res;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, AuthorFirstName, AuthorMiddleName, AuthorLastName, AuthorBirthDay")] Author author)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
            await client.PostAsync(url, stringContent);

            return RedirectToAction(nameof(Index));
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
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
            var res = JsonConvert.DeserializeObject<Author>(jsonStr);

            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, AuthorFirstName, AuthorMiddleName, AuthorLastName, AuthorBirthDay")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;// + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
            await client.PutAsync(url, stringContent);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
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
            var res = JsonConvert.DeserializeObject<Author>(jsonStr);

            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            await client.DeleteAsync(url);
            return RedirectToAction(nameof(Index));
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
            var author = JsonConvert.DeserializeObject<Author>(jsonStr);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
    }
}
