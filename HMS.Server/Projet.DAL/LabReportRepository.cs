using Projet.Context;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Projet.DAL
{
    public class LabReportRepository : ILabReportRepository
    {
        private readonly ApplicationDbContext _context;
        public LabReportRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<LabReport> GetAll() => _context.LabReports.ToList();

        public LabReport GetById(int id) => _context.LabReports.Find(id);

        public void Add(LabReport report)
        {
            _context.LabReports.Add(report);
            _context.SaveChanges();
        }

        public void Update(LabReport report)
        {
            _context.LabReports.Update(report);
            _context.SaveChanges();
    }
}}