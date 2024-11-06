using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexSolutions.DTOs; 
using ApexSolutions.Models; 
using ApexSolutions.Interfaces; 
using ApexSolutions.Repositories; 
namespace ApexSolutions.Services
{
    public class ServiceRequestService
    {
        private readonly IRepository<ServiceRequest> _serviceRequestRepository;

        // Constructor injection of the repository
        public ServiceRequestService(IRepository<ServiceRequest> serviceRequestRepository)
        {
            _serviceRequestRepository = serviceRequestRepository;
        }

        // Get all service requests
        public async Task<IEnumerable<ServiceRequestDTO>> GetAllServiceRequestsAsync()
        {
            var serviceRequests = await _serviceRequestRepository.GetAllAsync();
            return MapToDTO(serviceRequests); // Map to DTOs
        }

        // Get a specific service request by ID
        public async Task<ServiceRequestDTO> GetServiceRequestByIdAsync(int id)
        {
            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(id);
            return serviceRequest != null ? MapToDTO(serviceRequest) : null;
        }

        // Create a new service request
        public async Task<ServiceRequestDTO> CreateServiceRequestAsync(ServiceRequestDTO serviceRequestDto)
        {
            var serviceRequest = MapToModel(serviceRequestDto);
            var createdServiceRequest = await _serviceRequestRepository.AddAsync(serviceRequest);
            return MapToDTO(createdServiceRequest);
        }

        // Update an existing service request
        public async Task<ServiceRequestDTO> UpdateServiceRequestAsync(int id, ServiceRequestDTO serviceRequestDto)
        {
            var existingRequest = await _serviceRequestRepository.GetByIdAsync(id);
            if (existingRequest == null)
            {
                return null; // Request not found
            }

            // Update properties from DTO
            existingRequest.Description = serviceRequestDto.Description;
            existingRequest.Priority = serviceRequestDto.PriorityLevel;
            existingRequest.Status = serviceRequestDto.Status;
            existingRequest.ServiceType = serviceRequestDto.ServiceType; // Example property
            // Add any other properties you want to update

            var updatedRequest = await _serviceRequestRepository.UpdateAsync(existingRequest);
            return MapToDTO(updatedRequest);
        }

        // Delete a service request
        public async Task<bool> DeleteServiceRequestAsync(int id)
        {
            var existingRequest = await _serviceRequestRepository.GetByIdAsync(id);
            if (existingRequest == null)
            {
                return false; // Request not found
            }

            await _serviceRequestRepository.DeleteAsync(existingRequest.ServiceRequestID);
            return true;
        }

        // Mapping methods
        private ServiceRequestDTO MapToDTO(ServiceRequest serviceRequest)
        {
            return new ServiceRequestDTO
            {
                ServiceRequestID = serviceRequest.ServiceRequestID,
                ClientID = serviceRequest.ClientID,
                Description = serviceRequest.Description,
                ServiceType = serviceRequest.ServiceType,
                RequestDate = serviceRequest.RequestDate,
                Status = serviceRequest.Status,
                PriorityLevel = serviceRequest.Priority,
                // Map other properties as necessary
            };
        }

        private IEnumerable<ServiceRequestDTO> MapToDTO(IEnumerable<ServiceRequest> serviceRequests)
        {
            return serviceRequests.Select(request => MapToDTO(request)); // Map each ServiceRequest to ServiceRequestDTO
        }

        private ServiceRequest MapToModel(ServiceRequestDTO serviceRequestDto)
        {
            return new ServiceRequest
            {
                ServiceRequestID = serviceRequestDto.ServiceRequestID,
                ClientID = serviceRequestDto.ClientID,
                Description = serviceRequestDto.Description,
                ServiceType = serviceRequestDto.ServiceType,
                RequestDate = DateTime.UtcNow, // Set the current date/time
                Status = serviceRequestDto.Status ?? "Open", // Default to "Open" if Status is null
                Priority = serviceRequestDto.PriorityLevel,
                // Map other properties as necessary
            };
        }
    }
}