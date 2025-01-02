using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.BLL.Contract;
using Projet.Entities;

namespace HMS.Server.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentManager _manager;

        public AppointmentController(IAppointmentManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var appointments = _manager.GetAll();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var appointment = _manager.GetById(id);
            return appointment == null ? NotFound() : Ok(appointment);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAppointment = _manager.Add(appointment);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id }, createdAppointment);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest("Appointment ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingAppointment = _manager.GetById(id);
            if (existingAppointment == null)
                return NotFound();

            _manager.Update(appointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var appointment = _manager.GetById(id);
            if (appointment == null)
                return NotFound();

            _manager.Delete(id);
            return NoContent();
        }
    }
}