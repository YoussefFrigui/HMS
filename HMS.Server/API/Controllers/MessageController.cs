namespace HMS.Server.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Projet.BLL.Contract;
    using Projet.Entities;

    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageManager _manager;
        public MessageController(IMessageManager manager) => _manager = manager;

        [HttpGet]
        public IActionResult GetAll()
        {
            var messages = _manager.GetAll();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var msg = _manager.GetById(id);
            return msg == null ? NotFound() : Ok(msg);
        }

        [HttpPost]
        public IActionResult Create(Message message)
        {
            var created = _manager.Add(message);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}