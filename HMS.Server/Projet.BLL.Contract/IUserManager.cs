using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IUserManager
    {
        User Authenticate(string email, string password);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User CreateUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int id);

        User GetUserByEmail(string email);  // For authentication lookups
    }
}