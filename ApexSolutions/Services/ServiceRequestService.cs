using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexCare.Interfaces;
using ApexCare.Repositories;
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
