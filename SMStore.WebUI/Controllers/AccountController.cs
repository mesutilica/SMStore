using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;

namespace SMStore.WebUI.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(string email, string sifre)
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Customer customer)
        {
            return View();
        }

        public IActionResult SignOut()
        {
            return View();
        }
    }
}
