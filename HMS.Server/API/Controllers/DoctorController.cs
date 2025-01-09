using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.BLL;
using Projet.Entities;
using Projet.Enums;
using Projet.ViewModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Projet.Services;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireDoctorRole")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        private readonly AppointmentManager appointmentManager;

        private readonly LabReportManager labReportManager;

        private readonly MessageManager messageManager;

        private readonly MedicalHistoryManager medicalHistoryManager;
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
        public IActionResult CreateAppointment([FromBody] AppointmentViewModel model)
        {
            try
            {
                // Get doctorId from JWT claim
                var doctorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (doctorIdClaim == null)
                {
                    return BadRequest(new { message = "Doctor ID not found in token" });
                }

                // Verify doctor exists
                var doctorId = int.Parse(doctorIdClaim.Value);
                var doctor = _doctorService.GetDoctorById(doctorId);
                if (doctor == null)
                {
                    return NotFound(new { message = "Doctor not found" });
                }

                var appointment = new Appointment
                {
                    DoctorId = doctorId,
                    PatientId = model.PatientId,
                    PatientName = model.PatientName,
                    AppointmentDate = model.AppointmentDate,
                    Details = model.Details ?? string.Empty,
                    Status = AppointmentStatus.Scheduled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _doctorService.AddAppointment(appointment);
                return Ok(new { message = "Appointment created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                return BadRequest(new { message = "Error creating appointment", details = ex.Message });
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
                    Details = model.Details ?? string.Empty,
                    Status = model.Status,
                    UpdatedAt = DateTime.UtcNow
                };

                _doctorService.UpdateAppointment(appointment);
                return Ok(new { message = "Appointment updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating appointment");
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
                _logger.LogError(ex, "Error deleting appointment");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("appointments")]

        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            try
            {
                var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var appointments = _doctorService.GetAppointmentsForDoctor(doctorId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting appointments");
                return BadRequest(new { message = ex.Message });
            }
        }

       [HttpPost("lab-reports")]
public IActionResult CreateLabReport([FromBody] LabReportViewModel model)
{
    try
    {
        var doctorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (doctorIdClaim == null)
        {
            _logger.LogError("Doctor ID claim not found in token");
            return BadRequest(new { message = "Invalid token" });
        }

        var doctorIdparsed = int.Parse(doctorIdClaim.Value);
        var report = new LabReport
        {
            DoctorId = doctorIdparsed,
            PatientId = model.PatientId,
            ReportName = model.ReportName ?? throw new ArgumentNullException(nameof(model.ReportName)),
            ResultDetails = model.ResultDetails ?? string.Empty
        };

        _doctorService.AddLabReport(report);
        return Ok(new { message = "Lab report created successfully" });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error creating lab report");
        return BadRequest(new { message = "Error creating lab report", details = ex.Message });
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
                _logger.LogError(ex, "Error retrieving lab report");
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}