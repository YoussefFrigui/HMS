using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Projet.Services;
using Projet.Entities;
using Projet.ViewModel;
using Projet.Enums;
using HMS.Server.Projet.ViewModel;
using Projet.BLL;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequirePatientRole")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly ILogger<PatientController> _logger;

        private readonly AppointmentManager appointmentmanager;

        public PatientController(PatientService patientService, ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpPost("medical-history")]
        public IActionResult SubmitMedicalHistory([FromBody] MedicalHistoryViewModel model)
        {
            try
            {
                var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                    ?? throw new InvalidOperationException("Patient ID not found"));

                var history = new MedicalHistory
                {
                    PatientId = patientId,
                    HistoryDetails = model.HistoryDetails ?? string.Empty
                };

                _patientService.SubmitMedicalHistory(history);
                return Ok(new { message = "Medical history submitted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting medical history");
                return BadRequest(new { message = "Error submitting medical history", details = ex.Message });
            }
        }

        [HttpGet("medical-history/{patientId}")]
        public ActionResult<IEnumerable<MedicalHistory>> GetMedicalHistory(int patientId)
        {
            try
            {
                var history = _patientService.GetPatientMedicalHistory(patientId);
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
                var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                    ?? throw new InvalidOperationException("Patient ID not found"));

                var message = new Message
                {
                    SenderId = patientId,
                    RecipientId = model.ReceiverId,
                    Content = model.Content ?? string.Empty
                };

                _patientService.SendMessage(message);
                return Ok(new { message = "Message sent successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message");
                return BadRequest(new { message = "Error sending message", details = ex.Message });
            }
        }

      
[HttpPost("appointments")]
    public IActionResult CreateAppointment([FromBody] AppointmentViewModel model)
    {
        try
        {
            if (model == null)
                return BadRequest("Appointment data is required");

            var patientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (patientIdClaim == null)
                return BadRequest("Patient ID not found in token");

            var patientId = int.Parse(patientIdClaim.Value);
            var appointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = model.DoctorId,
                PatientName = model.PatientName ?? throw new ArgumentNullException(nameof(model.PatientName)),
                AppointmentDate = model.AppointmentDate,
                Details = model.Details ?? string.Empty,
                Status = AppointmentStatus.Scheduled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _patientService.CreateAppointment(appointment);
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
                var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                    ?? throw new InvalidOperationException("Patient ID not found"));

                var appointment = new Appointment
                {
                    Id = appointmentId,
                    PatientId = patientId,
                    DoctorId = model.DoctorId,
                    PatientName = model.PatientName,
                    AppointmentDate = model.AppointmentDate,
                    Details = model.Details ?? string.Empty,
                    Status = model.Status,
                    UpdatedAt = DateTime.UtcNow
                };

                appointmentmanager.Update(appointment);
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
                appointmentmanager.Delete(appointmentId);
                return Ok(new { message = "Appointment cancelled successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling appointment");
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}