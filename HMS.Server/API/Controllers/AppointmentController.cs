using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.Entities;
using Projet.Services;
using Projet.ViewModel;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentViewModel model)
        {
            try
            {
                var appointment = new Appointment
                {
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    AppointmentDate = model.AppointmentDate,
                    Details = model.Details
                };

                var result = await _appointmentService.CreateAppointment(appointment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentViewModel model)
        {
            try
            {
                var appointment = new Appointment
                {
                    Id = id,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    AppointmentDate = model.AppointmentDate,
                    Details = model.Details,
                    Status = model.Status
                };

                var result = await _appointmentService.UpdateAppointment(appointment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var result = await _appointmentService.CancelAppointment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetDoctorAppointments(int doctorId, [FromQuery] DateTime date)
        {
            var appointments = await _appointmentService.GetDoctorAppointments(doctorId, date);
            return Ok(appointments);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatientAppointments(int patientId)
        {
            var appointments = await _appointmentService.GetPatientAppointments(patientId);
            return Ok(appointments);
        }
    }
}