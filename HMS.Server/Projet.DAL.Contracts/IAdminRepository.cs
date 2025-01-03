using Projet.Entities;

namespace Projet.DAL.Contracts
{
    public interface IAdminRepository
    {
        void AddUser(User user);
        User GetUserById(int userId);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}