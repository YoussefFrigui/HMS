using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;

namespace Projet.BLL
{
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentManager(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _repository.GetAll();
        }

        public Appointment GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(Appointment appointment)
        {
            _repository.Add(appointment);
        }

        public void Update(Appointment appointment)
        {
            _repository.Update(appointment);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<Appointment> GetByDoctorId(int doctorId)
        {
            return _repository.GetByDoctorId(doctorId);
        }
    }
}