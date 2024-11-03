using Microsoft.AspNetCore.Mvc;
using ApexSolutions.DTOs;
using ApexSolutions.Services;
using ApexCare.Services;

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
        public async Task<IActionResult> GetAllServiceRequests()
        {
            var serviceRequests = await _serviceRequestService.GetAllServiceRequests();
            return Ok(serviceRequests);
        }

        // Get a specific service request by ID (GET request)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceRequestById(int id)
        {
            var serviceRequest = await _serviceRequestService.GetServiceRequestById(id);
            if (serviceRequest == null)
            {
                return NotFound();  // Return 404 if the service request is not found
            }
            return Ok(serviceRequest);
        }

        // Create a new service request (POST request)
        [HttpPost]
        public async Task<IActionResult> CreateServiceRequest([FromBody] ServiceRequestDTO serviceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return 400 if the data is invalid
            }

            var createdServiceRequest = await _serviceRequestService.CreateServiceRequest(serviceRequestDto);
            return CreatedAtAction(nameof(GetServiceRequestById), new { id = createdServiceRequest.ServiceRequestID }, createdServiceRequest);
        }

        // Update an existing service request (PUT request)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceRequest(int id, [FromBody] ServiceRequestDTO serviceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return 400 if the data is invalid
            }

            var updatedRequest = await _serviceRequestService.UpdateServiceRequest(id, serviceRequestDto);
            if (updatedRequest == null)
            {
                return NotFound();  // Return 404 if the service request doesn't exist
            }

            return NoContent();  // Return 204 if the update was successful
        }

        // Delete a service request (DELETE request)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            var result = await _serviceRequestService.DeleteServiceRequest(id);
            if (!result)
            {
                return NotFound();  // Return 404 if the service request is not found
            }
            return NoContent();  // Return 204 if the delete was successful
        }
    }
}
