using Projet.Entities;
using System.Collections.Generic;

namespace Projet.DAL.Contracts
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void Add(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(int id);
        IEnumerable<Appointment> GetByDoctorId(int doctorId);
    }
}