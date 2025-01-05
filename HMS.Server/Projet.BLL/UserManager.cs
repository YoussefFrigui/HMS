using Projet.BLL.Contract;
using Projet.DAL;
using Projet.DAL.Contracts;
using Projet.Entities;
using System;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;

        public UserManager(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            _repository.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public User GetById(int id)
        {
            var user = _repository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");
            return user;
        }

        public void Update(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
        }
    
        public User GetUserByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        
    }
}