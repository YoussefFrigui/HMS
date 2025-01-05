using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IAppointmentManager
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        IEnumerable<Appointment> GetDoctorAppointments(int doctorId);
        void Add(Appointment entity);
        void Update(Appointment entity);
        void Delete(int id);
    }
}