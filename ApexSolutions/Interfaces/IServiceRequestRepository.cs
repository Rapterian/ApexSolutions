using System.Collections.Generic;
using System.Threading.Tasks;
using ApexSolutions.Models;

namespace ApexSolutions.Interfaces
{
    public interface IServiceRequestRepository
    {
        // Create a new service request
        Task<ServiceRequest> AddAsync(ServiceRequest serviceRequest);

        // Get all service requests
        Task<IEnumerable<ServiceRequest>> GetAllAsync();

        // Get a service request by ID
        Task<ServiceRequest> GetByIdAsync(int serviceRequestId);

        // Update an existing service request
        Task<ServiceRequest> UpdateAsync(ServiceRequest serviceRequest);

        // Delete a service request
        Task<bool> DeleteAsync(int serviceRequestId);
    }
}