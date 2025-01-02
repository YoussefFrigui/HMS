using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IAppointmentManager
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        Appointment Add(Appointment appointment);
        Appointment Update(Appointment appointment);
        void Delete(int id);
        IEnumerable<Appointment> GetByDoctor(int doctorId);
    }
}