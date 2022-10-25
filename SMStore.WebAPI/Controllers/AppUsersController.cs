using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IRepository<AppUser> _repository;

        public AppUsersController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        // GET: api/<AppUsersController>
        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        // GET api/<AppUsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetAsync(int id)
        {
            var data = await _repository.FindAsync(id);
            if (data is null) return NotFound();
            return data;
        }

        // POST api/<AppUsersController>
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAsync([FromBody] AppUser appUser)
        {
            await _repository.AddAsync(appUser);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = appUser.Id}, appUser); // Ekleme işleminden sonra geriye eklenen kaydı döndürür.
        }

        // PUT api/<AppUsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, AppUser appUser)
        {
            _repository.Update(appUser);
            await _repository.SaveChangesAsync();
            return NoContent(); // Api kullanırken güncelleme işleminden sonra no content dönüş türü kullanılır.
        }

        // DELETE api/<AppUsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var data = _repository.Find(id);
            _repository.Delete(data);
            await _repository.SaveChangesAsync();
            return Ok(); // Geriye silme başarılı mesajı döndür
        }
    }
}
