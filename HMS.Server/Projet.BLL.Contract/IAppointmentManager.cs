using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL.Contract
{
    public interface IAppointmentManager
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);

        IEnumerable<Appointment> GetByDoctorId(int idd);
        IEnumerable<Appointment> GetDoctorAppointments(int id);

        //getall patietn appointmetns

        IEnumerable<Appointment> GetPatientAppointments(int id);
        void Add(Appointment entity);
        void Update(Appointment entity);
        void Delete(int id);
    }
}