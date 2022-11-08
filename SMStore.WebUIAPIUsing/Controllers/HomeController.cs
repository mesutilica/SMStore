using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.WebUIAPIUsing.Models;
using System.Diagnostics;

namespace SMStore.WebUIAPIUsing.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdresContact;
        private readonly string _apiAdresSlider;
        private readonly string _apiAdresProduct;
        private readonly string _apiAdresNews;
        private readonly string _apiAdresBrand;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdresContact = "https://localhost:7141/Api/Contacts";
            _apiAdresSlider = "https://localhost:7141/Api/Sliders";
            _apiAdresProduct = "https://localhost:7141/Api/Products";
            _apiAdresNews = "https://localhost:7141/Api/News";
            _apiAdresBrand = "https://localhost:7141/Api/Brands";
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel();
            model.Sliders = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdresSlider);
            model.Products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdresProduct);
            model.News = await _httpClient.GetFromJsonAsync<List<News>>(_apiAdresNews);
            model.Brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdresBrand);
            return View(model);
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiAdresContact, contact);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["mesaj"] = "<div class='alert alert-success'>Mesajınız Gönderildi.. Teşekkürler..</div>";
                        //return Redirect("/Home/ContactUs");
                        return Created("/Home/ContactUs", contact);
                    }
                        
                    else ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}