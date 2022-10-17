using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.WebUI.Utils;

namespace SMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly IRepository<Post> _repository;

        public PostsController(IRepository<Post> repository)
        {
            _repository = repository;
        }

        // GET: PostsController
        public async Task<IActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Post entity, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);
                    await _repository.AddAsync(entity);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(entity);
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Post entity, IFormFile? Image, bool? resmiSil)
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
            return View(entity);
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model);
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, Post post)
        {
            try
            {
                _repository.Delete(post);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
