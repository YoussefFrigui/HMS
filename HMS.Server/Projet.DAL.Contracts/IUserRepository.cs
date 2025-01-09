using Projet.Entities;
using Projet.Enums;
using System.Collections.Generic;

namespace Projet.DAL.Contracts
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetById(int id);
        User GetByEmail(string email);

        IEnumerable<User> GetByRole(Enums.Role role);
        void Update(User user);
        void Delete(int id);
        IEnumerable<User> GetAll();
    }
}