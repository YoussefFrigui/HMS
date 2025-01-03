using Projet.Context;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Projet.DAL
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User ValidatePatientLogin(string email, string password)
        {
            return _context.Users.FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password &&
                u.Role == Projet.Enums.Role.Patient
            );
        }

        public void AddMedicalHistory(MedicalHistory history)
        {
            _context.MedicalHistories.Add(history);
            _context.SaveChanges();
        }

        public IEnumerable<MedicalHistory> GetMedicalHistories(int patientId)
        {
            return _context.MedicalHistories
                .Where(m => m.PatientId == patientId)
                .ToList();
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<Message> GetMessagesBetweenUsers(int senderId, int receiverId)
        {
            return _context.Messages
                .Where(m => (m.SenderId == senderId && m.RecipientId == receiverId) ||
                            (m.SenderId == receiverId && m.RecipientId== senderId))
                .OrderBy(m => m.Id)
                .ToList();
        }
    }
}