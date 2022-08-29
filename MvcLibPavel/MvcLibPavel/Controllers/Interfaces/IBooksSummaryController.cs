using MvcLibPavel.Models;
using RestEase;
using System.Net.Http.Headers;

namespace MvcLibPavel.Controllers.Interfaces
{
    
    //[Header("User-Agent", "RestEase")]
    public interface IBooksSummaryController
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        [Get()]
        Task<string> GetAsync();
    }
}
