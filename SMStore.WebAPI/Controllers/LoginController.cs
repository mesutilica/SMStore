using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<AppUser> _repository;

        public LoginController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }
        // GET: LoginController
        [HttpGet]
        public async Task<ActionResult<AppUser>> GetAsync(string email, string password)
        {
            var data = await _repository.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsAdmin && u.IsActive);
            if (data is null) return NotFound();
            return data;
        }

    }
}
