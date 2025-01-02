namespace Projet.Entities
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string HistoryDetails { get; set; }
    }
}