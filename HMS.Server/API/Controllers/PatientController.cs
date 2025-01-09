using System.Security.Claims;
using HMS.Server.Projet.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.BLL;
using Projet.DAL;
using Projet.Entities;
using Projet.Services;
using Projet.ViewModel;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequirePatientRole")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        private readonly AppointmentManager appointmentManager;

        private readonly LabReportManager labReportManager;

        private readonly MedicalHistoryManager medicalHistoryManager;

        private readonly ILogger<PatientController> _logger;

        public PatientController(PatientService patientService, AppointmentManager appointmentManager, LabReportManager labReportManager, MedicalHistoryManager medicalHistoryManager)
        {
            _patientService = patientService;
        }


        /// <summary>
        /// Submit medical history.
        /// </summary>
        [HttpPost("medical-history")]
        public IActionResult SubmitMedicalHistory([FromBody] MedicalHistoryViewModel model)
        {
            // Construct an entity from the ViewModel.
            var history = new MedicalHistory
            {
                PatientId = model.PatientId,
                HistoryDetails = model.HistoryDetails
            };

            _patientService.SubmitMedicalHistory(history);
            return Ok("Medical history submitted successfully.");
        }

        /// <summary>
        /// Retrieve patient's medical history.
        /// </summary>
        [HttpGet("medical-history/{patientId}")]
        public IActionResult GetMedicalHistory(int patientId)
        {
            var histories = _patientService.GetPatientMedicalHistory(patientId);
            return Ok(histories);
        }

        /// <summary>
        /// Send a message to a doctor or admin.
        /// </summary>
        [HttpPost("send-message")]
        public IActionResult SendMessage([FromBody] MessageViewModel model)
        {
            var message = new Message
            {
                SenderId = model.SenderId,
                RecipientId = model.ReceiverId,
                Content = model.Content
            };
            _patientService.SendMessage(message);
            return Ok("Message sent successfully.");
        }

        /// <summary>
        /// Get messages between patient and another user.
        /// </summary>
        [HttpGet("messages/{senderId}/{receiverId}")]
        public IActionResult GetMessages(int senderId, int receiverId)
        {
            var messages = _patientService.GetMessages(senderId, receiverId);
            return Ok(messages);
        }
    

     [HttpPost("appointments")]
        public IActionResult CreateAppointment([FromBody] AppointmentViewModel model)
        {
            try
            {
                var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var appointment = new Appointment
                {
                    DoctorId = model.DoctorId,
                    PatientId = patientId,
                    PatientName = model.PatientName,
                    AppointmentDate = model.AppointmentDate,
                    Details = model.Details ?? string.Empty,
                    Status = model.Status,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                appointmentManager.Add(appointment);
                return Ok(new { message = "Appointment created successfully" });
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
                    Details = model.Details ?? string.Empty,
                    Status = model.Status,
                    UpdatedAt = DateTime.UtcNow
                };

                appointmentManager.Update(appointment);
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
                appointmentManager.Delete(appointmentId);
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
                var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var appointments = appointmentManager.GetPatientAppointments(patientId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting appointments");
                return BadRequest(new { message = ex.Message });
            }
        }
    



    
}
}