using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Enums;

namespace Projet.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateUser(string email, string password, Role role)
        {
            var user = new User
            {
                Email = email,
                Password = PasswordHasher.HashPassword(password),
                Role = role
            };
            _userRepository.Add(user);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }
    }
}