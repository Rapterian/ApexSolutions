using Microsoft.AspNetCore.Mvc;
using ApexSolutions.DTOs;
using ApexSolutions.Services;

namespace ApexCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly ServiceRequestService _serviceRequestService;

        // Inject the ServiceRequestService through constructor injection
        public ServiceRequestController(ServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        // Get a list of all service requests (GET request)
        [HttpGet]
        public IActionResult GetAllServiceRequests()
        {
            var serviceRequests = _serviceRequestService.GetAllServiceRequests();
            return Ok(serviceRequests);
        }

        // Get a specific service request by ID (GET request)
        [HttpGet("{id}")]
        public IActionResult GetServiceRequestById(int id)
        {
            var serviceRequest = _serviceRequestService.GetServiceRequestById(id);
            if (serviceRequest == null)
            {
                return NotFound();  // Return 404 if the service request is not found
            }
            return Ok(serviceRequest);
        }

        // Create a new service request (POST request)
        [HttpPost]
        public IActionResult CreateServiceRequest([FromBody] ServiceRequestDTO serviceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return 400 if the data is invalid
            }

            var createdServiceRequest = _serviceRequestService.CreateServiceRequest(serviceRequestDto);
            return CreatedAtAction(nameof(GetServiceRequestById), new { id = createdServiceRequest.Id }, createdServiceRequest);
        }

        // Update an existing service request (PUT request)
        [HttpPut("{id}")]
        public IActionResult UpdateServiceRequest(int id, [FromBody] ServiceRequestDTO serviceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return 400 if the data is invalid
            }

            var updatedRequest = _serviceRequestService.UpdateServiceRequest(id, serviceRequestDto);
            if (updatedRequest == null)
            {
                return NotFound();  // Return 404 if the service request doesn't exist
            }

            return NoContent();  // Return 204 if the update was successful
        }

        // Delete a service request (DELETE request)
        [HttpDelete("{id}")]
        public IActionResult DeleteServiceRequest(int id)
        {
            var result = _serviceRequestService.DeleteServiceRequest(id);
            if (!result)
            {
                return NotFound();  // Return 404 if the service request is not found
            }
            return NoContent();  // Return 204 if the delete was successful
        }
    }
}
