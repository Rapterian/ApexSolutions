using ApexSolutions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexSolutions.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<ServiceRequest> GetByIdAsync(int serviceRequestId);
        Task<List<ServiceRequest>> GetAllAsync();
        Task<ServiceRequest> AddAsync(ServiceRequest serviceRequest);
        Task<ServiceRequest> UpdateAsync(ServiceRequest serviceRequest);
        Task<bool> DeleteAsync(int serviceRequestId);
    }
}
