using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class MedicalHistoryManager : IMedicalHistoryManager
    {
        private readonly IMedicalHistoryRepository _repo;
        public MedicalHistoryManager(IMedicalHistoryRepository repo) => _repo = repo;

        public IEnumerable<MedicalHistory> GetAll() => _repo.GetAll();
        public MedicalHistory GetById(int id) => _repo.GetById(id);
        public MedicalHistory Add(MedicalHistory history)
        {
            _repo.Add(history);
            return history;
        }
        public MedicalHistory Update(MedicalHistory history)
        {
            _repo.Update(history);
            return history;
        }
        public void Delete(int id) => _repo.Delete(id);
    }
}