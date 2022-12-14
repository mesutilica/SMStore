using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IRepository<Contact> _repository;

        public ContactsController(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetAsync(int id)
        {
            var data = await _repository.FindAsync(id);
            if (data is null) return NotFound();
            return data;
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<ActionResult<Contact>> PostAsync([FromBody] Contact entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, Contact entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var data = _repository.Find(id);
            _repository.Delete(data);
            await _repository.SaveChangesAsync();
            return Ok();
        }
    }
}
