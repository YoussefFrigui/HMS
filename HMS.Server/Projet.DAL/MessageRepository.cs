using Projet.Context;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Projet.DAL
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Message> GetAll() => _context.Messages.ToList();

        public Message GetById(int id) => _context.Messages.Find(id);

        public void Add(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }
    }
}