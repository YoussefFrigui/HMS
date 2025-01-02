using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface ILabReportManager
    {
        IEnumerable<LabReport> GetAll();
        LabReport GetById(int id);
        LabReport Add(LabReport report);
    }
}