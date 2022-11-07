using Microsoft.AspNetCore.Mvc;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _brandRepository.GetAllAsync(b => b.IsActive);

            if (model == null) return NotFound();

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _brandRepository.MarkayiUrunlerliyleGetir(id);

            if (model == null) return NotFound();

            return View(model);
        }

    }
}
