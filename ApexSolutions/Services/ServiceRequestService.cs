using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexCare.Interfaces;
using ApexCare.Repositories;
using ApexSolutions.DTOs;
using ApexSolutions.Models;

namespace ApexCare.Services
{
    public class ServiceRequestService
    {
        private readonly IRepository<ServiceRequest> _serviceRequestRepository;

        public ServiceRequestService(IRepository<ServiceRequest> serviceRequestRepository)
        {
            _serviceRequestRepository = serviceRequestRepository;
        }

        // Create a new service request
        public async Task<ServiceRequest> CreateServiceRequest(ServiceRequest serviceRequest)
        {
            // TODO - include validation logic here
            // TODO - Set timestamp
            return await _serviceRequestRepository.AddAsync(serviceRequest);
        }
        public async Task<ServiceRequest> CreateServiceRequest(ServiceRequestDTO serviceRequestDto)
        {
            // Create a new instance of ServiceRequest using the parameterless constructor
            var newRequest = new ServiceRequest
            {
                ClientID = serviceRequestDto.ClientID,
                Description = serviceRequestDto.Description,
                // You need to decide how to set ServiceType, RequestDate, and Location
                ServiceType = "DefaultServiceType", // Set a default or map accordingly
                RequestDate = DateTime.UtcNow, // Set the current date/time
                Status = serviceRequestDto.Status ?? "Open", // Default to "Open" if Status is null
                Priority = serviceRequestDto.PriorityLevel, // Map PriorityLevel to Priority
                                                            // Other properties can be set to null or default values as needed
            };

            // Optionally set AssignedTechnicianID and ResolutionTimestamp if needed
            // newRequest.TechnicianID = serviceRequestDto.AssignedTechnicianID;

            // Call repository to add the new request
            return await _serviceRequestRepository.AddAsync(newRequest);
        }

        // Get all service requests
        public async Task<IEnumerable<ServiceRequest>> GetAllServiceRequests()
        {
            return await _serviceRequestRepository.GetAllAsync();
        }

        // Get a service request by ID
        public async Task<ServiceRequest> GetServiceRequestById(int id)
        {
            return await _serviceRequestRepository.GetByIdAsync(id);
        }

        // Update an existing service request
        public async Task<ServiceRequest> UpdateServiceRequest(ServiceRequest serviceRequest)
        {
            // TODO - include validation logic here
            return await _serviceRequestRepository.UpdateAsync(serviceRequest);
        }

        public async Task<ServiceRequest> UpdateServiceRequest(int id, ServiceRequestDTO serviceRequestDto)
        {
            // Retrieve the existing service request
            var existingRequest = await GetServiceRequestById(id);
            if (existingRequest == null)
            {
                return null; // Return null if not found
            }

            // Update properties from DTO
            existingRequest.Description = serviceRequestDto.Description;
            existingRequest.Priority = serviceRequestDto.PriorityLevel;
            existingRequest.Status = serviceRequestDto.Status;

            // Call the repository to update
            return await _serviceRequestRepository.UpdateAsync(existingRequest);
        }

        // Delete a service request
        public async Task<bool> DeleteServiceRequest(int id)
        {
            return await _serviceRequestRepository.DeleteAsync(id);
        }

        // Additional methods for specific business logic
        public async Task AssignTechnician(int requestId, int technicianId)
        {
            var serviceRequest = await GetServiceRequestById(requestId);
            if (serviceRequest != null)
            {
                serviceRequest.AssignTechnician(technicianId);
                await UpdateServiceRequest(serviceRequest);
            }
        }

        public async Task CloseRequest(int requestId)
        {
            var serviceRequest = await GetServiceRequestById(requestId);
            if (serviceRequest != null)
            {
                serviceRequest.CloseRequest();
                await UpdateServiceRequest(serviceRequest);
            }
        }
    }
}
