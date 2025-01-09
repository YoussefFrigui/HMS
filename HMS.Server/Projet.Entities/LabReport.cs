using System.ComponentModel.DataAnnotations;

namespace Projet.Entities
{
   public class LabReport
{
    public int Id { get; set; }
    
    [Required]
    public int PatientId { get; set; }
    
    [Required]
    public int DoctorId { get; set; }
    
    [Required]
    [StringLength(100)]
    public required string ReportName { get; set; }
    
    public string ResultDetails { get; set; } = string.Empty;
}
}