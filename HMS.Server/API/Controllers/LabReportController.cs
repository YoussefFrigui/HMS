namespace HMS.Server.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    
    using Microsoft.AspNetCore.Authorization;
    using global::Projet.BLL.Contract;
    using global::Projet.Entities;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LabReportController : ControllerBase
    {
        private readonly ILabReportManager _manager;
        public LabReportController(ILabReportManager manager) => _manager = manager;

        [HttpGet]
        public IActionResult GetAll()
        {
            var reports = _manager.GetAll();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var report = _manager.GetById(id);
            return report == null ? NotFound() : Ok(report);
        }

        [HttpPost]
        public IActionResult Create(LabReport report)
        {
            _manager.Add(report);
            return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
        }
    }
}