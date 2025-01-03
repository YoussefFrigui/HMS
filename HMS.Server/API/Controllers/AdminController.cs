using Microsoft.AspNetCore.Mvc;
using Projet.Enums;
using Projet.Services;
using Projet.ViewModel;
using Projet.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] UserViewModel model)
        {
            _userService.CreateUser(model.Email, model.Password, Enum.Parse<Role>(model.Role, true));
            return Ok("User added successfully!");
        }

        [HttpGet("view-user/{userId}")]
        public IActionResult ViewUser(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPut("update-user")]
        public IActionResult UpdateUser([FromBody] UserViewModel model)
        {
            // Map UserViewModel to User entity
            var user = new User
            {
                Id = model.Id,
                Email = model.Email,
                Password = model.Password,
                Role = Enum.Parse<Role>(model.Role, true) // Parse string to Role enum
            };

            _userService.UpdateUser(user);
            return Ok("User updated successfully!");
        }

        [HttpDelete("delete-user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            return Ok("User deleted successfully!");
        }
    }
}