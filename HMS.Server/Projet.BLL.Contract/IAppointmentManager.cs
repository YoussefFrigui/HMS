using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IAppointmentManager
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment> GetById(int id);
        Task<IEnumerable<Appointment>> GetDoctorAppointments(int doctorId);
    }
}