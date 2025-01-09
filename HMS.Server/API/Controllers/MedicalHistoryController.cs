/*namespace HMS.Server.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using global::Projet.Entities;
    using global::Projet.BLL.Contract;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMedicalHistoryManager _manager;
        public MedicalHistoryController(IMedicalHistoryManager manager) => _manager = manager;

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _manager.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _manager.GetById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Create(MedicalHistory history)
        {
            _manager.Add(history);
            return CreatedAtAction(nameof(GetById), new { id = history.Id }, history);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, MedicalHistory history)
        {
            if (id != history.Id) return BadRequest();
            _manager.Update(history);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _manager.Delete(id);
            return NoContent();
        }
    }
}*/