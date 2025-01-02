using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentManager(IAppointmentRepository repo) => _repo = repo;

        public IEnumerable<Appointment> GetAll() => _repo.GetAll();
        public Appointment GetById(int id) => _repo.GetById(id);
        public Appointment Add(Appointment appointment)
        {
            _repo.Add(appointment);
            return appointment;
        }
        public Appointment Update(Appointment appointment)
        {
            _repo.Update(appointment);
            return appointment;
        }
        public void Delete(int id) => _repo.Delete(id);
        public IEnumerable<Appointment> GetByDoctor(int doctorId) => _repo.GetByDoctorId(doctorId);
    }
}