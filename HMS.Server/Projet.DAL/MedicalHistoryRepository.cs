using Projet.Context;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Projet.DAL
{
    public class MedicalHistoryRepository : IMedicalHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicalHistoryRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<MedicalHistory> GetAll() => _context.MedicalHistories.ToList();

        public MedicalHistory GetById(int id) => _context.MedicalHistories.Find(id);

        public void Add(MedicalHistory history)
        {
            _context.MedicalHistories.Add(history);
            _context.SaveChanges();
        }

        public void Update(MedicalHistory history)
        {
            _context.MedicalHistories.Update(history);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var history = _context.MedicalHistories.Find(id);
            if (history != null)
            {
                _context.MedicalHistories.Remove(history);
                _context.SaveChanges();
            }
        }
    }
}