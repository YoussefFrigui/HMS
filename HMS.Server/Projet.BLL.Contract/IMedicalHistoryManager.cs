using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IMedicalHistoryManager
    {
        IEnumerable<MedicalHistory> GetAll();
        MedicalHistory GetById(int id);
        MedicalHistory Add(MedicalHistory history);
        MedicalHistory Update(MedicalHistory history);
        void Delete(int id);
    }
}