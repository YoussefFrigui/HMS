using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Enums;
using Projet.Exceptions;
using System;
using System.Collections.Generic;

namespace Projet.Services
{
    public class AppointmentService 
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public void Add(Appointment appointment)
        {
            ValidateAppointment(appointment);
            
            if (_appointmentRepository.HasConflict(appointment.DoctorId, appointment.AppointmentDate))
                throw new AppointmentConflictException("Doctor already has an appointment at this time");

            appointment.Status = AppointmentStatus.Scheduled;
            appointment.CreatedAt = DateTime.UtcNow;
            
            _appointmentRepository.Add(appointment);
        }

        public Appointment GetById(int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
                throw new KeyNotFoundException($"Appointment with ID {id} not found");
            return appointment;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public void Update(Appointment appointment)
        {
            ValidateAppointment(appointment);
            
            var existing = _appointmentRepository.GetById(appointment.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Appointment with ID {appointment.Id} not found");

            if (appointment.AppointmentDate != existing.AppointmentDate && 
                _appointmentRepository.HasConflict(appointment.DoctorId, appointment.AppointmentDate))
                throw new AppointmentConflictException("Doctor already has an appointment at this time");

            appointment.UpdatedAt = DateTime.UtcNow;
            _appointmentRepository.Update(appointment);
        }

        public void Delete(int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
                throw new KeyNotFoundException($"Appointment with ID {id} not found");

            _appointmentRepository.Delete(id);
        }

        private void ValidateAppointment(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            if (appointment.AppointmentDate < DateTime.UtcNow)
                throw new ArgumentException("Cannot schedule appointments in the past");

            if (string.IsNullOrWhiteSpace(appointment.PatientName))
                throw new ArgumentException("Patient name is required");

            if (string.IsNullOrWhiteSpace(appointment.Details))
                throw new ArgumentException("Appointment details are required");

            if (appointment.DoctorId <= 0)
                throw new ArgumentException("Invalid doctor ID");

            if (appointment.PatientId <= 0)
                throw new ArgumentException("Invalid patient ID");
        }
    }
}