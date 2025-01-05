using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using System;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class MedicalHistoryManager : IMedicalHistoryManager
    {
        private readonly IMedicalHistoryRepository _repository;

        public MedicalHistoryManager(IMedicalHistoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(MedicalHistory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            var history = GetById(id);
            _repository.Delete(id);
        }

        public IEnumerable<MedicalHistory> GetAll()
        {
            return _repository.GetAll();
        }

        public MedicalHistory GetById(int id)
        {
            var history = _repository.GetById(id);
            if (history == null)
                throw new KeyNotFoundException($"Medical History with ID {id} not found");
            return history;
        }

        public void Update(MedicalHistory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
        }
    }
}