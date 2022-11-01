using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;

namespace SMStore.WebUIAPIUsing.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdresProduct;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdresProduct = "https://localhost:7141/Api/Products";
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdresProduct + "/" + id);
            return View(model);
        }
    }
}
