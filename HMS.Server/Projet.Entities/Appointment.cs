namespace Projet.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public System.DateTime AppointmentDate { get; set; }
    }
}