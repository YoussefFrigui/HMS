using Projet.Entities;
using System.Collections.Generic;

namespace Projet.DAL.Contracts
{
    public interface IDoctorRepository
    {
        // Authentication
        User ValidateDoctorLogin(string email, string password);

        // View patient medical history
        IEnumerable<MedicalHistory> GetMedicalHistoryByPatientId(int patientId);

        // Messaging
        void AddMessage(Message message);
        IEnumerable<Message> GetMessagesBetweenUsers(int senderId, int receiverId);

        // Schedule Management
        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int appointmentId);
        IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId);

        // Lab Reports
        void AddLabReport(LabReport report);
        LabReport GetLabReportById(int labReportId);
        IEnumerable<LabReport> GetLabReportsForPatient(int patientId);

        //get doctorbyid

         User GetDoctorById(int doctorId);
    }
}