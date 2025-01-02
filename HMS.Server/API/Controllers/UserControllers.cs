namespace HMS.Server.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Projet.BLL.Contract;
    using Projet.Entities;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _manager;
        public UserController(IUserManager manager) => _manager = manager;

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _manager.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _manager.GetUserById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            var created = _manager.CreateUser(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            if (id != user.Id) return BadRequest();
            _manager.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _manager.DeleteUser(id);
            return NoContent();
        }
    }
}