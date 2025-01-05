using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IUserManager
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User entity);
        void Update(User entity);
        void Delete(int id);
        User GetUserByEmail(string email);  // For authentication lookups

    }
}