using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Services;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null || !PasswordHasher.VerifyPassword(password, user.Password))
            {
                return null;
            }
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User CreateUser(User user)
        {
            _userRepository.Add(user);
            return user;
        }

        public User UpdateUser(User user)
        {
            _userRepository.Update(user);
            return user;
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }
    }
}