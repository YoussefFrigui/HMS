using System;
using Projet.Enums;

namespace Projet.ViewModel
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Details { get; set; }
        public AppointmentStatus Status { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
    }
}