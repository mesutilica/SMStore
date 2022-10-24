using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var model = await _repository.FindAsync(id.Value);
            return View(model);
        }

        public async Task<IActionResult> SearchAsync(string kelime)
        {
            var model = await _repository.GetAllAsync(p => p.Name.Contains(kelime) || p.Description.Contains(kelime));

            ViewBag.Baslik = kelime;

            return View(model);
        }
    }
}
