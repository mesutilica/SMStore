using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;

namespace SMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public AppUsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "https://localhost:7141/api/AppUsers";
        }

        // GET: AppUsersController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdres); // _httpClient nesnesi Api ye istekleri göndermek için kullanılır. GetFromJsonAsync metodu ise _apiAdres deki api mize yaptığı istek sonucu gelen json data yı List<AppUser> yani app user listesine dönüştürü ve view a gönderir.
            return View(model);
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
