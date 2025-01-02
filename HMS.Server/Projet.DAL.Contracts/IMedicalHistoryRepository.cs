using Projet.Entities;
using System.Collections.Generic;

namespace Projet.DAL.Contracts
{
    public interface IMedicalHistoryRepository
    {
        IEnumerable<MedicalHistory> GetAll();
        MedicalHistory GetById(int id);
        void Add(MedicalHistory history);
        void Update(MedicalHistory history);
        void Delete(int id);
    }
}