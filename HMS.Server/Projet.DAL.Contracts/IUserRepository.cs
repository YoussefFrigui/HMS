using Projet.Entities;
using System.Collections.Generic;

namespace Projet.DAL.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        User GetByEmail(string email);
    }
}