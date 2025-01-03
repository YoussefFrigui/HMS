using Projet.Context;
using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Projet.DAL
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Authentication
        public User ValidateDoctorLogin(string email, string password)
        {
            return _context.Users.FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password &&
                u.Role == Role.Doctor
            );
        }

        // View patient medical history
        public IEnumerable<MedicalHistory> GetMedicalHistoryByPatientId(int patientId)
        {
            return _context.MedicalHistories
                .Where(m => m.PatientId == patientId)
                .ToList();
        }

        // Messaging
        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<Message> GetMessagesBetweenUsers(int senderId, int receiverId)
        {
            return _context.Messages
                .Where(m =>
                    (m.SenderId == senderId && m.RecipientId == receiverId) ||
                    (m.SenderId == receiverId && m.RecipientId == senderId)
                )
                .OrderBy(m => m.Id)
                .ToList();
        }

        // Schedule Management
        public void AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void DeleteAppointment(int appointmentId)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            return _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToList();
        }

        // Lab Reports
        public void AddLabReport(LabReport report)
        {
            _context.LabReports.Add(report);
            _context.SaveChanges();
        }

        public LabReport GetLabReportById(int labReportId)
        {
            return _context.LabReports.Find(labReportId);
        }

        public IEnumerable<LabReport> GetLabReportsForPatient(int patientId)
        {
            return _context.LabReports
                .Where(r => r.PatientId == patientId)
                .ToList();
        }
    }
}