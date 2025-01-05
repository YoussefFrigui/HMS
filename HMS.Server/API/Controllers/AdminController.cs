using Microsoft.AspNetCore.Mvc;
using Projet.Enums;
using Projet.BLL.Contract;
using Projet.ViewModel;
using Projet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Projet.Services;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AdminController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] RegisterModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid client request");

            var user = new User
            {
                Email = model.Email,
                Password = PasswordHasher.HashPassword(model.Password),
                Role = model.Role
            };

            _userManager.Add(user);
            return Ok();
        }

        [HttpGet("view-user/{userId}")]
        public IActionResult ViewUser(int userId)
        {
            var user = _userManager.GetById(userId);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPut("update-user")]
        public IActionResult UpdateUser([FromBody] UserViewModel model)
        {
            var user = new User
            {
                Id = model.Id,
                Email = model.Email,
                Password = PasswordHasher.HashPassword(model.Password),
                Role = Enum.Parse<Role>(model.Role, true)
            };

            _userManager.Update(user);
            return Ok("User updated successfully!");
        }

        [HttpDelete("delete-user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            _userManager.Delete(userId);
            return Ok("User deleted successfully!");
        }
    }
}