
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Projet.BLL.Contract;
using Projet.ViewModel;
using Projet.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Projet.Services;
using Projet.Enums;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IConfiguration _config;

        public AuthController(IUserManager userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

          [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = _userManager.GetUserByEmail(model.Email);
            if (userExists != null)
            {
                return BadRequest(new { message = "User already exists!" });
            }

            var user = new User
            {
                Email = model.Email,
                Password = PasswordHasher.HashPassword(model.Password),
                Role = Role.Patient // Default role
            };

            _userManager.Add(user);

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userManager.GetUserByEmail(loginModel.Email);
            if (user == null || !Projet.Services.PasswordHasher.VerifyPassword(loginModel.Password, user.Password))
                return Unauthorized("Invalid credentials");

            var token = GenerateJWTToken(user);
            return Ok(new { Token = token });
        }

        // AuthController.cs
private string GenerateJWTToken(User user)
{
    var secret = _config["Jwt:Secret"];
    if (string.IsNullOrEmpty(secret))
    {
        throw new ArgumentNullException("Jwt:Secret", "JWT Secret is not configured.");
    }

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role.ToString()) // Ensure Role is a string like "Admin", "Doctor", "Patient"
    };

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiryMinutes"])),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
}
}