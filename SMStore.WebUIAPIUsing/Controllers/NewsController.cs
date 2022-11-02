using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;

namespace SMStore.WebUIAPIUsing.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdresNews;
        public NewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdresNews = "https://localhost:7141/Api/News";
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<News>>(_apiAdresNews);

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<News>(_apiAdresNews + "/" + id);

            return View(model);
        }
    }
}
