using System;
using Projet.Enums;

namespace Projet.Entities
{
   public class Appointment
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public required string Details { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual User Doctor { get; set; } = null!;
    public virtual User Patient { get; set; } = null!;
}}