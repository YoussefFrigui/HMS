using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;

namespace Projet.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // Authentication
        public User LoginDoctor(string email, string password)
        {
            return _doctorRepository.ValidateDoctorLogin(email, password);
        }

        // Medical history
        public IEnumerable<MedicalHistory> GetPatientMedicalHistory(int patientId)
        {
            return _doctorRepository.GetMedicalHistoryByPatientId(patientId);
        }

        // Messaging
        public void SendMessage(Message message)
        {
            _doctorRepository.AddMessage(message);
        }

        public IEnumerable<Message> GetMessagesBetweenUsers(int senderId, int receiverId)
        {
            return _doctorRepository.GetMessagesBetweenUsers(senderId, receiverId);
        }

        // Schedule
        public void AddAppointment(Appointment appointment)
        {
            _doctorRepository.AddAppointment(appointment);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _doctorRepository.UpdateAppointment(appointment);
        }

        public void DeleteAppointment(int appointmentId)
        {
            _doctorRepository.DeleteAppointment(appointmentId);
        }

        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            return _doctorRepository.GetAppointmentsForDoctor(doctorId);
        }

        // Lab Reports
        public void AddLabReport(LabReport report)
        {
            _doctorRepository.AddLabReport(report);
        }

        public LabReport GetLabReportById(int labReportId)
        {
            return _doctorRepository.GetLabReportById(labReportId);
        }

        public IEnumerable<LabReport> GetLabReportsForPatient(int patientId)
        {
            return _doctorRepository.GetLabReportsForPatient(patientId);
        }
    }
}