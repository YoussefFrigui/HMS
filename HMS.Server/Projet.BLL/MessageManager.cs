using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class MessageManager : IMessageManager
    {
        private readonly IMessageRepository _repo;
        public MessageManager(IMessageRepository repo) => _repo = repo;

        public IEnumerable<Message> GetAll() => _repo.GetAll();
        public Message GetById(int id) => _repo.GetById(id);
        public Message Add(Message message)
        {
            _repo.Add(message);
            return message;
        }
    }
}