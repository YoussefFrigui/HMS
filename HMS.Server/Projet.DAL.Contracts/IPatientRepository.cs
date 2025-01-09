using Projet.Entities;
using System.Collections.Generic;

namespace Projet.DAL.Contracts
{
    public interface IPatientRepository
    {
        // Login: verifies patient credentials
        User ValidatePatientLogin(string email, string password);

        // Medical history
        void AddMedicalHistory(MedicalHistory history);
        IEnumerable<MedicalHistory> GetMedicalHistories(int patientId);

        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int appointmentId);


        // Messaging
        void AddMessage(Message message);
        IEnumerable<Message> GetMessagesBetweenUsers(int senderId, int receiverId);
    }
}