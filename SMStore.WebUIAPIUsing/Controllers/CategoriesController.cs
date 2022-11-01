using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;

namespace SMStore.WebUIAPIUsing.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdresProduct;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdresProduct = "https://localhost:7141/Api/Products";
        }
        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdresProduct + "/" + id);

            if (model == null) return NotFound();

            return View(model);
        }
    }
}
