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

    public async Task<IEnumerable<Appointment>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Appointment> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<IEnumerable<Appointment>> GetDoctorAppointments(int doctorId)
    {
        return await _repository.GetDoctorAppointments(doctorId, DateTime.Now);
    }
}}