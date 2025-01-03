using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.Entities;
using Projet.Enums;
using Projet.Services;
using Projet.ViewModel;
using System;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// Doctor login.
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _doctorService.LoginDoctor(model.Email, model.Password);
            if (user == null)
                return Unauthorized("Invalid credentials or not a Doctor.");

            return Ok($"Doctor {user.Email} logged in successfully.");
        }

        /// <summary>
        /// View a patient's medical history.
        /// </summary>
        [HttpGet("medical-history/{patientId}")]
        public IActionResult ViewMedicalHistory(int patientId)
        {
            var history = _doctorService.GetPatientMedicalHistory(patientId);
            return Ok(history);
        }

        /// <summary>
        /// Send a message to a patient or admin.
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
            _doctorService.SendMessage(message);
            return Ok("Message sent successfully (Doctor).");
        }

        /// <summary>
        /// Create a new appointment.
        /// </summary>
        [HttpPost("appointments")]
        public IActionResult CreateAppointment([FromBody] AppointmentViewModel model)
        {
            var appointment = new Appointment
            {
                DoctorId = model.DoctorId,
                AppointmentDate = model.AppointmentDate,
                Details = model.Details
            };
            _doctorService.AddAppointment(appointment);
            return Ok("Appointment created successfully!");
        }

        /// <summary>
        /// Update an existing appointment.
        /// </summary>
        [HttpPut("appointments/{appointmentId}")]
        public IActionResult UpdateAppointment(int appointmentId, [FromBody] AppointmentViewModel model)
        {
            var appointment = new Appointment
            {
                Id = appointmentId,
                DoctorId = model.DoctorId,
                AppointmentDate = model.AppointmentDate,
                Details = model.Details
            };
            _doctorService.UpdateAppointment(appointment);
            return Ok("Appointment updated successfully!");
        }

        /// <summary>
        /// Delete an existing appointment.
        /// </summary>
        [HttpDelete("appointments/{appointmentId}")]
        public IActionResult DeleteAppointment(int appointmentId)
        {
            _doctorService.DeleteAppointment(appointmentId);
            return Ok("Appointment deleted successfully!");
        }

        /// <summary>
        /// Add a new lab report for a patient.
        /// </summary>
        [HttpPost("lab-reports")]
        public IActionResult AddLabReport([FromBody] LabReportViewModel model)
        {
            var report = new LabReport
            {
                PatientId = model.PatientId,
                ReportName = model.ReportName,
                ResultDetails = model.ResultDetails
            };
            _doctorService.AddLabReport(report);
            return Ok("Lab report added successfully!");
        }

        /// <summary>
        /// Get details of a specific lab report.
        /// </summary>
        [HttpGet("lab-reports/{labReportId}")]
        public IActionResult GetLabReport(int labReportId)
        {
            var report = _doctorService.GetLabReportById(labReportId);
            if (report == null)
                return NotFound("Lab report not found.");

            return Ok(report);
        }
    }
}