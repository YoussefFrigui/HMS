using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projet.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<bool> CreateAppointment(Appointment appointment)
        {
            if (await _appointmentRepository.HasConflict(appointment.DoctorId, appointment.AppointmentDate))
                throw new Exception("Doctor already has an appointment at this time.");

            appointment.Status = AppointmentStatus.Scheduled;
            return await _appointmentRepository.Add(appointment);
        }

        public async Task<bool> UpdateAppointment(Appointment appointment)
        {
            var existing = await _appointmentRepository.GetById(appointment.Id);
            if (existing == null)
                throw new Exception("Appointment not found.");

            return await _appointmentRepository.Update(appointment);
        }

        public async Task<bool> CancelAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            if (appointment == null)
                throw new Exception("Appointment not found.");

            appointment.Status = AppointmentStatus.Cancelled;
            return await _appointmentRepository.Update(appointment);
        }

        public async Task<IEnumerable<Appointment>> GetDoctorAppointments(int doctorId, DateTime date)
        {
            return await _appointmentRepository.GetDoctorAppointments(doctorId, date);
        }

        public async Task<IEnumerable<Appointment>> GetPatientAppointments(int patientId)
        {
            return await _appointmentRepository.GetPatientAppointments(patientId);
        }
    }
}