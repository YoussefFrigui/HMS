using Projet.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projet.DAL.Contracts
{
    public interface IAppointmentRepository
{
    void Add(Appointment appointment);
    Appointment? GetById(int id);
    IEnumerable<Appointment> GetAll();
    void Update(Appointment appointment);
    void Delete(int id);
    bool HasConflict(int doctorId, DateTime appointmentDate);
        IEnumerable<Appointment> GetDoctorAppointments(int doctorId, DateTime now);



    IEnumerable<Appointment> GetByDoctorId(int doctorId);
    IEnumerable<Appointment> GetByPatientId(int patientId);
        IEnumerable<Appointment> GetPatientAppointments(int patientId);
    }
}