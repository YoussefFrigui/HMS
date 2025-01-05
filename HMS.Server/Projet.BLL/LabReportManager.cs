using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using System;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class LabReportManager : ILabReportManager
    {
        private readonly ILabReportRepository _repository;

        public LabReportManager(ILabReportRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(LabReport entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            var report = GetById(id);
            _repository.Delete(id);
        }

        public IEnumerable<LabReport> GetAll()
        {
            return _repository.GetAll();
        }

        public LabReport GetById(int id)
        {
            var report = _repository.GetById(id);
            if (report == null)
                throw new KeyNotFoundException($"Lab Report with ID {id} not found");
            return report;
        }

        public void Update(LabReport entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
        }
    }
}