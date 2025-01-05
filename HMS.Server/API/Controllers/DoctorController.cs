using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.BLL;
using Projet.Entities;
using Projet.Enums;
using Projet.Services;
using Projet.ViewModel;
using System;
using System.Security.Claims;

namespace Projet.API.Controllers
{
  [ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireDoctorRole")]
public class DoctorController : ControllerBase
{
    private readonly DoctorService _doctorService;
    private readonly ILogger<DoctorController> _logger;

    public DoctorController(DoctorService doctorService, ILogger<DoctorController> logger)
    {
        _doctorService = doctorService;
        _logger = logger;
    }

    [HttpGet("medical-history/{patientId}")]
    public ActionResult<IEnumerable<MedicalHistory>> GetMedicalHistory(int patientId)
    {
        try
        {
            var history = _doctorService.GetPatientMedicalHistory(patientId);
            return Ok(history);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting medical history for patient {PatientId}", patientId);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("send-message")]
    public ActionResult SendMessage([FromBody] MessageViewModel model)
    {
        try
        {
            var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var message = new Message
            {
                SenderId = doctorId,
                RecipientId = model.ReceiverId,
                Content = model.Content,
                
            };
            
            _doctorService.SendMessage(message);
            return Ok(new { message = "Message sent successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending message");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("appointments")]
public async Task<IActionResult> CreateAppointment([FromBody] AppointmentViewModel model)
{
    try
    {
        var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var doctorEmail = User.FindFirst(ClaimTypes.Email)?.Value;

        var appointment = new Appointment
        {
            DoctorId = doctorId,
            PatientId = model.PatientId,
            PatientName = model.PatientName,
            AppointmentDate = model.AppointmentDate,
            Details = model.Details ?? string.Empty,
            Status = AppointmentStatus.Scheduled,
            CreatedAt = DateTime.UtcNow
        };

        await _doctorService.AddAppointment(appointment);
        return Ok(new { message = "Appointment created successfully" });
    }
    catch (AppointmentConflictException ex)
    {
        return Conflict(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error creating appointment");
        return BadRequest(new { message = ex.Message });
    }
}

    [HttpPut("appointments/{appointmentId}")]
    public ActionResult UpdateAppointment(int appointmentId, [FromBody] AppointmentViewModel model)
    {
        try
        {
            var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var appointment = new Appointment
            {
                Id = appointmentId,
                DoctorId = doctorId,
                PatientId = model.PatientId,
                PatientName = model.PatientName,
                AppointmentDate = model.AppointmentDate,
                Details = model.Details,
                Status = model.Status
            };

            _doctorService.UpdateAppointment(appointment);
            return Ok(new { message = "Appointment updated successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appointment {AppointmentId}", appointmentId);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("appointments/{appointmentId}")]
    public ActionResult DeleteAppointment(int appointmentId)
    {
        try
        {
            _doctorService.DeleteAppointment(appointmentId);
            return Ok(new { message = "Appointment deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting appointment {AppointmentId}", appointmentId);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("lab-reports")]
    public ActionResult CreateLabReport([FromBody] LabReportViewModel model)
    {
        try
        {
            var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var report = new LabReport
            {
                PatientId = model.PatientId,
                ReportName = model.ReportName,
                ResultDetails = model.ResultDetails,
                
            };

            _doctorService.AddLabReport(report);
            return Ok(new { message = "Lab report created successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating lab report");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("lab-reports/{labReportId}")]
    public ActionResult<LabReport> GetLabReport(int labReportId)
    {
        try
        {
            var report = _doctorService.GetLabReportById(labReportId);
            if (report == null)
                return NotFound(new { message = "Lab report not found" });

            return Ok(report);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting lab report {LabReportId}", labReportId);
            return BadRequest(new { message = ex.Message });
        }
    }
}
}