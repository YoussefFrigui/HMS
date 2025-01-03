using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Enums;

namespace Projet.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public void CreateUser(string email, string password, Role role)
        {
            var user = new User
            {
                Email = email,
                Password = password,
                Role = role
            };
            _adminRepository.AddUser(user);
        }

        public User GetUserById(int userId)
        {
            return _adminRepository.GetUserById(userId);
        }

        public void UpdateUser(User user)
        {
            _adminRepository.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            _adminRepository.DeleteUser(userId);
        }
    }
}