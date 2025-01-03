using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Services;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepo;
        public UserManager(IUserRepository userRepo) => _userRepo = userRepo;

        public IEnumerable<User> GetAllUsers() => _userRepo.GetAll();
        public User GetUserById(int id) => _userRepo.GetById(id);

        public User CreateUser(User user)
        {
            _userRepo.Add(user);
            return user;
        }

        public User UpdateUser(User user)
        {
            _userRepo.Update(user);
            return user;
        }

        public void DeleteUser(int id) => _userRepo.Delete(id);

        public User GetUserByEmail(string email) => _userRepo.GetByEmail(email);

        public User Authenticate(string email, string password)
        {
            var user = _userRepo.GetByEmail(email);
            if (user == null || !PasswordHasher.VerifyPassword(password, user.Password)){

                return null;
        }
            return user;
        }
    }
}
