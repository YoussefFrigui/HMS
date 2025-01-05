namespace Projet.Entities
{
    public class LabReport
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public int DoctorId { get; set; }    
        public string ReportName { get; set; }
        public string ResultDetails { get; set; }
    }
}