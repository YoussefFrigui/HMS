using Projet.Entities;
using System.Collections.Generic;

namespace Projet.DAL.Contracts
{
    public interface ILabReportRepository
    {
        IEnumerable<LabReport> GetAll();
        LabReport GetById(int id);
        void Add(LabReport report);
        void Update(LabReport report);
        
    }
}