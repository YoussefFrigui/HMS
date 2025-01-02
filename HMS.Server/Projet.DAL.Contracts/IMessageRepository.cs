using Projet.Entities;
using System.Collections.Generic;

namespace Projet.DAL.Contracts
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetAll();
        Message GetById(int id);
        void Add(Message message);
        // Potentially define more queries: e.g. GetBySender, etc.
    }
}