using Microsoft.AspNetCore.Mvc;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;
        // InvalidOperationException: Unable to resolve service for type 'SMStore.Service.Repositories.ICategoryRepository' while attempting to activate 'SMStore.WebUI.Controllers.CategoriesController'.
        // Eğer yukarıdaki gibi bir hata alırsak hatayla ilgili interface ve class ı servis olarak program.cs de tanımlamamışız demektir!!!
        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _repository.KategoriyiUrunlerliyleGetir(id);

            if (model == null) return NotFound();

            return View(model);
        }
    }
}
