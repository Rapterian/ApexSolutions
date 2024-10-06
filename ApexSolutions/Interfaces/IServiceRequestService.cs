using ApexSolutions.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexSolutions.Services
{
    public interface IServiceRequestService
    {
        Task<ServiceRequestDTO> GetServiceRequestByIdAsync(int serviceRequestId);
        Task<List<ServiceRequestDTO>> GetAllServiceRequestsAsync();
        Task<ServiceRequestDTO> CreateServiceRequestAsync(ServiceRequestDTO serviceRequestDto);
        Task<ServiceRequestDTO> UpdateServiceRequestAsync(int serviceRequestId, ServiceRequestDTO serviceRequestDto);
        Task<bool> DeleteServiceRequestAsync(int serviceRequestId);
    }
}
