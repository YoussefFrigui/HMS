using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class LabReportManager : ILabReportManager
    {
        private readonly ILabReportRepository _repo;
        public LabReportManager(ILabReportRepository repo) => _repo = repo;

        public IEnumerable<LabReport> GetAll() => _repo.GetAll();
        public LabReport GetById(int id) => _repo.GetById(id);
        public LabReport Add(LabReport report)
        {
            _repo.Add(report);
            return report;
        }
        public LabReport Update(LabReport report)
        {
            _repo.Update(report);
            return report;
        }
    }
}