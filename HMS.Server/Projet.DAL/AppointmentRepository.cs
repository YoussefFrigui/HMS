using Projet.Context;
using Projet.DAL.Contracts;
using Projet.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Projet.DAL
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Appointment> GetAll() => _context.Appointments.ToList();

        public Appointment GetById(int id) => _context.Appointments.Find(id);

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var app = _context.Appointments.Find(id);
            if (app != null)
            {
                _context.Appointments.Remove(app);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Appointment> GetByDoctorId(int doctorId) =>
            _context.Appointments.Where(a => a.DoctorId == doctorId).ToList();
    }
}