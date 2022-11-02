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
            var model = await _brandRepository.MarkayiUrunlerliyleGetir(id);

            return View(model);
        }

    }
}
