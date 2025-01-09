using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Enums;
using Projet.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projet.BLL
{
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentManager(IAppointmentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(Appointment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Validate appointment
            ValidateAppointment(entity);

            // Add appointment
            _repository.Add(entity);
        }

        private void ValidateAppointment(Appointment appointment)
        {
            // Check for conflicting appointments
            var doctorAppointments = _repository.GetDoctorAppointments(
                appointment.DoctorId, 
                appointment.AppointmentDate.Date);

            var conflicts = doctorAppointments.Where(a => 
                a.AppointmentDate.Hour == appointment.AppointmentDate.Hour &&
                a.Status != AppointmentStatus.Cancelled);

            if (conflicts.Any())
                throw new AppointmentConflictException("Doctor already has an appointment at this time");

            // Validate appointment time
            if (appointment.AppointmentDate < DateTime.Now)
                throw new ArgumentException("Cannot schedule appointments in the past");
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _repository.GetAll();
        }

        public Appointment GetById(int id)
        {
            var appointment = _repository.GetById(id);
            if (appointment == null)
                throw new KeyNotFoundException($"Appointment with ID {id} not found");
            return appointment;
        }

        public IEnumerable<Appointment> GetByPatientId(int patientId)
{
    return _repository.GetByPatientId(patientId);
}



        public IEnumerable<Appointment> GetDoctorAppointments(int doctorId)
        {
            if (doctorId <= 0)
                throw new ArgumentException("Invalid doctor ID");

            return _repository.GetDoctorAppointments(doctorId, DateTime.Now);
        }

        public void Update(Appointment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            ValidateAppointment(entity);
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            var appointment = GetById(id);
            _repository.Delete(id);
        }
        //GetByDoctorId

        public IEnumerable<Appointment> GetByDoctorId(int doctorId)
        {
            return _repository.GetByDoctorId(doctorId);
        }

        public IEnumerable<Appointment> GetPatientAppointments(int patientId)
        {
            return _repository.GetPatientAppointments(patientId);
        }
    }
}