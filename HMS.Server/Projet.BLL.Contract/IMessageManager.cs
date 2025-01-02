using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IMessageManager
    {
        IEnumerable<Message> GetAll();
        Message GetById(int id);
        Message Add(Message message);
    }
}