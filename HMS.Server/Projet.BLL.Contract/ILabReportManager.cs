using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface ILabReportManager
    {
        IEnumerable<LabReport> GetAll();
        LabReport GetById(int id);
        void Add(LabReport entity);
        void Update(LabReport entity);
        void Delete(int id);
    }
}