using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IMedicalHistoryManager
    {
        IEnumerable<MedicalHistory> GetAll();
        MedicalHistory GetById(int id);
        void Add(MedicalHistory entity);
        void Update(MedicalHistory entity);
        void Delete(int id);
    }
}