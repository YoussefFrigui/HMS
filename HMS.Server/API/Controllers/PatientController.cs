using HMS.Server.Projet.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Patient login with email and password.
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _patientService.LoginPatient(model.Email, model.Password);
            if (user == null)
                return Unauthorized("Invalid credentials or not a Patient.");
            
            return Ok($"Patient {user.Email} logged in successfully.");
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
    }
}