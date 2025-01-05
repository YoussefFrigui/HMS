using Microsoft.EntityFrameworkCore;
using Projet.Context;
using Projet.DAL.Contracts;
using Projet.Entities;
using Projet.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projet.DAL
{
   public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        _context.SaveChanges();
    }

    public Appointment? GetById(int id)
    {
        return _context.Appointments.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Appointment> GetAll()
    {
        return _context.Appointments.ToList();
    }

    public void Update(Appointment appointment)
    {
        _context.Entry(appointment).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var appointment = GetById(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }
    }

    public bool HasConflict(int doctorId, DateTime appointmentDate)
    {
        return _context.Appointments.Any(a => 
            a.DoctorId == doctorId && 
            a.AppointmentDate.Date == appointmentDate.Date &&
            a.Status != AppointmentStatus.Cancelled);
    }

    public IEnumerable<Appointment> GetDoctorAppointments(int doctorId, DateTime now)
    {
        return _context.Appointments.Where(a => 
            a.DoctorId == doctorId && 
            a.AppointmentDate.Date == now.Date);
    }
}}