using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using System;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class MessageManager : IMessageManager
    {
        private readonly IMessageRepository _repository;

        public MessageManager(IMessageRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(Message entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Add(entity);
        }

       
        public IEnumerable<Message> GetAll()
        {
            return _repository.GetAll();
        }

        public Message GetById(int id)
        {
            var message = _repository.GetById(id);
            if (message == null)
                throw new KeyNotFoundException($"Message with ID {id} not found");
            return message;
        }

    
    }
}