using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly IRepository<News> _repository;

        public NewsController(IRepository<News> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }
        public async Task<IActionResult> DetailAsync(int id)
        {
            var model = await _repository.FindAsync(id);

            if (model is null) return NotFound();

            return View(model);
        }
    }
}
