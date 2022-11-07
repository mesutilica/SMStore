using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using Newtonsoft.Json; // Bu paketi nuget dan yükledik. Json dan class a, class dan json a sorunsuz dönüştürme işlemi yapar.

namespace SMStore.WebUIAPIUsing.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "https://localhost:7141/Api/Categories";
        }
        public async Task<IActionResult> IndexAsync(int id)
        {
            /* Tek kategori nesnesi döbüdürürsek aşağıdaki kod çalışıyor
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "/GetCategoryByProducts/" + id);

            if (model == null) return NotFound();

            return View(model);
            */

            var categories = await _httpClient.GetAsync(_apiAdres + "/GetCategoryByProducts/" + id); // apiye istek attık
            if (categories.IsSuccessStatusCode) // api den dönen istek başarılı sonuç döndürürse
            {
                var response = await categories.Content.ReadAsStringAsync(); // categories den den dönen içeriği string formatında oku
                var model = JsonConvert.DeserializeObject<Category>(response); // model değişkenine Newtonsoft paketinin JsonConvert sınıfındaki DeserializeObject metoduyla api den dönen response yani cevap içerisindeki Json datayı Category sınıfından bir nesneye dönüştür
                return View(model);
            }
            return NotFound();
        }
    }
}
