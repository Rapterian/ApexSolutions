using System;
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
            _serviceRequestRepository = serviceRequestRepository ?? throw new ArgumentNullException(nameof(serviceRequestRepository), "Service request repository cannot be null");
        }

        // Get all service requests
        public async Task<IEnumerable<ServiceRequestDTO>> GetAllServiceRequestsAsync()
        {
            try
            {
                var serviceRequests = await _serviceRequestRepository.GetAllAsync();
                return MapToDTO(serviceRequests);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching service requests: {ex.Message}");
                return Enumerable.Empty<ServiceRequestDTO>();
            }
        }

        // Get a specific service request by ID
        public async Task<ServiceRequestDTO> GetServiceRequestByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid service request ID");
            }

            try
            {
                var serviceRequest = await _serviceRequestRepository.GetByIdAsync(id);
                return serviceRequest != null ? MapToDTO(serviceRequest) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching service request with ID {id}: {ex.Message}");
                return null;
            }
        }

        // Create a new service request
        public async Task<ServiceRequestDTO> CreateServiceRequestAsync(ServiceRequestDTO serviceRequestDto)
        {
            if (serviceRequestDto == null)
            {
                throw new ArgumentNullException(nameof(serviceRequestDto), "ServiceRequestDTO cannot be null");
            }

            try
            {
                var serviceRequest = MapToModel(serviceRequestDto);
                var createdServiceRequest = await _serviceRequestRepository.AddAsync(serviceRequest);
                return MapToDTO(createdServiceRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating service request: {ex.Message}");
                return null;
            }
        }

        // Update an existing service request
        public async Task<ServiceRequestDTO> UpdateServiceRequestAsync(int id, ServiceRequestDTO serviceRequestDto)
        {
            if (serviceRequestDto == null)
            {
                throw new ArgumentNullException(nameof(serviceRequestDto), "ServiceRequestDTO cannot be null");
            }

            try
            {
                var existingRequest = await _serviceRequestRepository.GetByIdAsync(id);
                if (existingRequest == null)
                {
                    Console.WriteLine($"Service request with ID {id} not found");
                    return null;
                }

                // Update properties from DTO
                existingRequest.Description = serviceRequestDto.Description;
                existingRequest.Priority = serviceRequestDto.PriorityLevel;
                existingRequest.Status = serviceRequestDto.Status;
                existingRequest.ServiceType = serviceRequestDto.ServiceType;

                var updatedRequest = await _serviceRequestRepository.UpdateAsync(existingRequest);
                return MapToDTO(updatedRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating service request with ID {id}: {ex.Message}");
                return null;
            }
        }

        // Delete a service request
        public async Task<bool> DeleteServiceRequestAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid service request ID");
            }

            try
            {
                var existingRequest = await _serviceRequestRepository.GetByIdAsync(id);
                if (existingRequest == null)
                {
                    Console.WriteLine($"Service request with ID {id} not found");
                    return false;
                }

                await _serviceRequestRepository.DeleteAsync(existingRequest.ServiceRequestID);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting service request with ID {id}: {ex.Message}");
                return false;
            }
        }

        // Mapping methods
        private ServiceRequestDTO MapToDTO(ServiceRequest serviceRequest)
        {
            if (serviceRequest == null)
            {
                throw new ArgumentNullException(nameof(serviceRequest), "ServiceRequest cannot be null");
            }

            return new ServiceRequestDTO
            {
                ServiceRequestID = serviceRequest.ServiceRequestID,
                ClientID = serviceRequest.ClientID,
                Description = serviceRequest.Description,
                ServiceType = serviceRequest.ServiceType,
                RequestDate = serviceRequest.RequestDate,
                Status = serviceRequest.Status,
                PriorityLevel = serviceRequest.Priority,
            };
        }

        private IEnumerable<ServiceRequestDTO> MapToDTO(IEnumerable<ServiceRequest> serviceRequests)
        {
            if (serviceRequests == null)
            {
                throw new ArgumentNullException(nameof(serviceRequests), "ServiceRequest list cannot be null");
            }

            return serviceRequests.Select(request => MapToDTO(request));
        }

        private ServiceRequest MapToModel(ServiceRequestDTO serviceRequestDto)
        {
            if (serviceRequestDto == null)
            {
                throw new ArgumentNullException(nameof(serviceRequestDto), "ServiceRequestDTO cannot be null");
            }

            return new ServiceRequest
            {
                ServiceRequestID = serviceRequestDto.ServiceRequestID,
                ClientID = serviceRequestDto.ClientID,
                Description = serviceRequestDto.Description,
                ServiceType = serviceRequestDto.ServiceType,
                RequestDate = DateTime.UtcNow,
                Status = serviceRequestDto.Status ?? "Open",
                Priority = serviceRequestDto.PriorityLevel,
            };
        }
    }
}
