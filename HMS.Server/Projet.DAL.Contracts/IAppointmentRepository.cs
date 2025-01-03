using Projet.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projet.DAL.Contracts
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetById(int id);
        Task<IEnumerable<Appointment>> GetAll();
        Task<IEnumerable<Appointment>> GetDoctorAppointments(int doctorId, DateTime date);
        Task<IEnumerable<Appointment>> GetPatientAppointments(int patientId);
        Task<bool> Add(Appointment appointment);
        Task<bool> Update(Appointment appointment);
        Task<bool> Delete(int id);
        Task<bool> HasConflict(int doctorId, DateTime appointmentDate);
    }
}