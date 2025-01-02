namespace HMS.Server.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Projet.BLL.Contract;
    using Projet.Entities;

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentManager _manager;
        public AppointmentController(IAppointmentManager manager) => _manager = manager;

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
        public IActionResult Create(Appointment appointment)
        {
            var created = _manager.Add(appointment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Appointment appointment)
        {
            if (id != appointment.Id) return BadRequest();
            _manager.Update(appointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _manager.Delete(id);
            return NoContent();
        }

        [HttpGet("doctor/{doctorId}")]
        public IActionResult GetByDoctor(int doctorId)
        {
            var apps = _manager.GetByDoctor(doctorId);
            return Ok(apps);
        }
    }
}