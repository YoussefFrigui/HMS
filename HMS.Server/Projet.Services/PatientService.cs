using Projet.BLL;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;

namespace Projet.Services
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;

        private readonly AppointmentManager appointmentManager;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // Login logic for patients
        public User LoginPatient(string email, string password)
        {
            return _patientRepository.ValidatePatientLogin(email, password);
        }

        // Medical history
        public void SubmitMedicalHistory(MedicalHistory history)
        {
            _patientRepository.AddMedicalHistory(history);
        }

        public IEnumerable<MedicalHistory> GetPatientMedicalHistory(int patientId)
        {
            return _patientRepository.GetMedicalHistories(patientId);
        }

        // Messaging
        public void SendMessage(Message message)
        {
            _patientRepository.AddMessage(message);
        }

        public IEnumerable<Message> GetMessages(int senderId, int receiverId)
        {
            return _patientRepository.GetMessagesBetweenUsers(senderId, receiverId);
        }

          public void CreateAppointment(Appointment appointment)
    {
        
            appointmentManager.Add(appointment);
        
        
    }
}
}