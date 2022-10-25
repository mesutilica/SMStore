using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _repository;

        public PostsController(IRepository<Post> repository)
        {
            _repository = repository;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public async Task<IEnumerable<Post>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetAsync(int id)
        {
            var data = await _repository.FindAsync(id);
            if (data is null) return NotFound();
            return data;
        }

        // POST api/<PostsController>
        [HttpPost]
        public async Task<ActionResult<News>> PostAsync([FromBody] Post entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, Post entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = _repository.Find(id);
            _repository.Delete(data);
            await _repository.SaveChangesAsync();
            return Ok();
        }
    }
}
