using Projet.BLL.Contract;
using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Enums;
using Projet.Exceptions;
using System.Collections.Generic;

namespace Projet.BLL
{
public class AppointmentManager : IAppointmentManager
{
    private readonly IAppointmentRepository _repository;

    public AppointmentManager(IAppointmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Add(Appointment entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        // Validate appointment
        await ValidateAppointment(entity);

        // Add appointment
        await _repository.Add(entity);
    }

    private async Task ValidateAppointment(Appointment appointment)
    {
        // Check for conflicting appointments
        var doctorAppointments = await _repository.GetDoctorAppointments(
            appointment.DoctorId, 
            appointment.AppointmentDate.Date);

        var conflicts = doctorAppointments.Where(a => 
            a.AppointmentDate.Hour == appointment.AppointmentDate.Hour &&
            a.Status != AppointmentStatus.Cancelled);

        if (conflicts.Any())
            throw new AppointmentConflictException("Doctor already has an appointment at this time");

        // Validate appointment time
        if (appointment.AppointmentDate < DateTime.Now)
            throw new ArgumentException("Cannot schedule appointments in the past");
    }

    public async Task<IEnumerable<Appointment>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Appointment> GetById(int id)
    {
        var appointment = await _repository.GetById(id);
        if (appointment == null)
            throw new KeyNotFoundException($"Appointment with ID {id} not found");
        return appointment;
    }

    public async Task<IEnumerable<Appointment>> GetDoctorAppointments(int doctorId)
    {
        if (doctorId <= 0)
            throw new ArgumentException("Invalid doctor ID");

        return await _repository.GetDoctorAppointments(doctorId, DateTime.Now);
    }

    public async Task Update(Appointment entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await ValidateAppointment(entity);
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        var appointment = await GetById(id);
        await _repository.Delete(id);
    }
}
}
