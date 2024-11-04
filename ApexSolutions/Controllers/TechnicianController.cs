using Microsoft.AspNetCore.Mvc;
using ApexSolutions.DTOs; // Assuming you have a DTO for Technician
using ApexSolutions.Services; // Assuming the TechnicianService and TechnicianAssignmentService are in this namespace
using System.Threading.Tasks;

namespace ApexSolutions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnicianController : ControllerBase
    {
        private readonly TechnicianService _technicianService;
        private readonly TechnicianAssignmentService _technicianAssignmentService;

        // Inject the TechnicianService and TechnicianAssignmentService through constructor injection
        public TechnicianController(TechnicianService technicianService, TechnicianAssignmentService technicianAssignmentService)
        {
            _technicianService = technicianService;
            _technicianAssignmentService = technicianAssignmentService;
        }

        // Get a list of all technicians (GET request)
        [HttpGet]
        public async Task<IActionResult> GetAllTechnicians()
        {
            var technicians = await _technicianService.GetAllTechniciansAsync();
            return Ok(technicians);
        }

        // Get a specific technician by ID (GET request)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTechnicianById(int id)
        {
            var technician = await _technicianService.GetTechnicianByIdAsync(id);
            if (technician == null)
            {
                return NotFound();  // Return 404 if the technician is not found
            }
            return Ok(technician);
        }

        // Create a new technician (POST request)
        [HttpPost]
        public async Task<IActionResult> CreateTechnician([FromBody] TechnicianDTO technicianDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return 400 if the data is invalid
            }

            var createdTechnician = await _technicianService.CreateTechnicianAsync(technicianDto);
            return CreatedAtAction(nameof(GetTechnicianById), new { id = createdTechnician.TechnicianID }, createdTechnician);
        }

        // Update an existing technician (PUT request)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTechnician(int id, [FromBody] TechnicianDTO technicianDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return 400 if the data is invalid
            }

            var updatedTechnician = await _technicianService.UpdateTechnicianAsync(id, technicianDto);
            if (updatedTechnician == null)
            {
                return NotFound();  // Return 404 if the technician doesn't exist
            }

            return NoContent();  // Return 204 if the update was successful
        }

        // Delete a technician (DELETE request)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnician(int id)
        {
            var result = await _technicianService.DeleteTechnicianAsync(id);
            if (!result)
            {
                return NotFound();  // Return 404 if the technician is not found
            }
            return NoContent();  // Return 204 if the delete was successful
        }

        // Assign a technician to a service request (POST request)
        [HttpPost("assign")]
        public async Task<IActionResult> AssignTechnician([FromBody] AssignTechnicianRequest request)
        {
            if (request == null || request.ServiceRequestId <= 0 || request.TechnicianId <= 0)
            {
                return BadRequest("Invalid request data.");
            }

            var result = await _technicianAssignmentService.AssignTechnicianAsync(request.ServiceRequestId, request.TechnicianId);
            if (!result)
            {
                return NotFound(); // Return 404 if the service request or technician is not found or not available
            }

            return Ok("Technician assigned successfully.");
        }
    }

    // DTO for assignment request
    public class AssignTechnicianRequest
    {
        public int ServiceRequestId { get; set; }
        public int TechnicianId { get; set; }
    }
}