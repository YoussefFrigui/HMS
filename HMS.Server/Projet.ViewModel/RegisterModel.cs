using System.ComponentModel.DataAnnotations;
using Projet.Enums;
namespace Projet.ViewModel
{
    public class RegisterModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}