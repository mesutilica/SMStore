using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.WebUI.Utils;

namespace SMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Category> _repositoryCategory;
        private readonly IRepository<Brand> _repositoryBrand;

        public ProductsController(IRepository<Product> repository, IRepository<Category> repositoryCategory, IRepository<Brand> repositoryBrand)
        {
            _repository = repository;
            _repositoryCategory = repositoryCategory;
            _repositoryBrand = repositoryBrand;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<IActionResult> CreateAsync()
        {
            var kategoriler = await _repositoryCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
            var markalar = await _repositoryBrand.GetAllAsync();
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product product, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) product.Image = await FileHelper.FileLoaderAsync(Image);
                    await _repository.AddAsync(product);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            var kategoriler = await _repositoryCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
            var markalar = await _repositoryBrand.GetAllAsync();
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
            return View(product);
        }

        // GET: ProductsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            var kategoriler = await _repositoryCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
            var markalar = await _repositoryBrand.GetAllAsync();
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
            return View(model);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Product entity, IFormFile? Image, bool? resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resmiSil == true) entity.Image = string.Empty;
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);
                    _repository.Update(entity);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            var kategoriler = await _repositoryCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
            var markalar = await _repositoryBrand.GetAllAsync();
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
            return View(entity);
        }

        // GET: ProductsController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Product entity)
        {
            try
            {
                _repository.Delete(entity);
                _repository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
