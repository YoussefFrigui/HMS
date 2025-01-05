using System;
using Projet.Enums;

namespace Projet.ViewModel
{
    public class AppointmentViewModel
{
    public int PatientId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Details { get; set; } = string.Empty;
    public AppointmentStatus Status { get; set; }
    public string PatientName { get; set; } = string.Empty;
}
}