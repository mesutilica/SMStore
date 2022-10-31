using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.WebUIAPIUsing.Models;
using System.Security.Claims;

namespace SMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "https://localhost:7141/Api/Login";
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(AdminLoginViewModel model)
        {
            try
            {
                var account = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "?email=" + model.Email + "&password=" + model.Password);
                if (account == null)
                {
                    ModelState.AddModelError("", "Giriş Başarısız!");
                }
                else
                {
                    // Eğer verilen bilgilerle eşleşen kullancı veritabanında varsa
                    var claims = new List<Claim>() // kullanıcıya haklar tanımlıyoruz
                    {
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim("Role", account.IsAdmin ? "Admin" : "User"), // eğer adminse admin hakkı değilse user hakkı verdik
                        new Claim("UserId", account.Id.ToString())
                    };
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin/Main");
                }
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", hata.Message + " Hatası Oluştu!");
            }
            return View();
        }

        [Route("Admin/Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(); // Çıkış yap
            return RedirectToAction("Index"); // login e yönlendir
        }
    }
}
