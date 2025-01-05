using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.BLL;
using Projet.BLL.Contract;
using Projet.Entities;
using Projet.Enums;
using Projet.Exceptions;
using Projet.Services;
using Projet.ViewModel;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
   [ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentManager _appointmentManager;

    public AppointmentController(IAppointmentManager appointmentManager)
    {
        _appointmentManager = (AppointmentManager?)(appointmentManager ?? throw new ArgumentNullException(nameof(appointmentManager)));
    }

          [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAll()
        {
            try
            {
                var appointments = _appointmentManager.GetAll();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> GetById(int id)
        {
            try
            {
                var appointment = _appointmentManager.GetById(id);
                return Ok(appointment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Appointment {id} not found" });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] AppointmentViewModel model)
        {
            try
            {
                var appointment = new Appointment
                {
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    PatientName = model.PatientName,
                    AppointmentDate = model.AppointmentDate,
                    Details = model.Details ?? string.Empty,
                    Status = AppointmentStatus.Scheduled,
                    CreatedAt = DateTime.UtcNow,
                    Doctor = new User { Id = model.DoctorId },
                    Patient = new User { Id = model.PatientId }
                };
                
                _appointmentManager.Add(appointment);
                return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
            }
            catch (AppointmentConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AppointmentViewModel model)
        {
            try
            {
                var appointment = new Appointment
                {
                    Id = id,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    PatientName = model.PatientName,
                    AppointmentDate = model.AppointmentDate,
                    Details = model.Details ?? string.Empty,
                    Status = model.Status,
                    UpdatedAt = DateTime.UtcNow,
                    Doctor = new User { Id = model.DoctorId },
                    Patient = new User { Id = model.PatientId }
                };

                _appointmentManager.Update(appointment);
                return Ok(new { message = "Appointment updated successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Appointment {id} not found" });
            }
            catch (AppointmentConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _appointmentManager.Delete(id);
                return Ok(new { message = "Appointment deleted successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Appointment {id} not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
