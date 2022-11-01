using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public Categories(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "https://localhost:7141/Api/Categories";
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);

            return View(categories);
        }
    }
}
