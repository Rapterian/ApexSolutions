using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ApexSolutions.Models;
using ApexSolutions.Interfaces;
using ApexSolutions.Data;

namespace ApexSolutions.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly DatabaseContext _dbContext;

        public ServiceRequestRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceRequest> AddAsync(ServiceRequest serviceRequest)
        {
            var sql = "InsertServiceRequest"; // Name of the stored procedure
            var parameters = new
            {
                serviceRequest.ClientID,
                serviceRequest.TechnicianID,
                serviceRequest.Description,
                serviceRequest.ServiceType,
                RequestDate = serviceRequest.RequestDate,
                serviceRequest.Status,
                serviceRequest.Priority,
                serviceRequest.EstimatedCompletionTime,
                serviceRequest.Location
            };
            var id = await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
            serviceRequest.ServiceRequestID = id; // Assuming ServiceRequest has a ServiceRequestID property
            return serviceRequest;
        }

        public async Task<IEnumerable<ServiceRequest>> GetAllAsync()
        {
            var sql = "GetServiceRequests"; // Name of the stored procedure
            return await _dbContext.QueryAsync<ServiceRequest>(sql, commandType: CommandType.StoredProcedure);
        }

        public async Task<ServiceRequest> GetByIdAsync(int serviceRequestId)
        {
            var sql = "GetServiceRequestById"; // Assuming you have a stored procedure for this
            var parameters = new { ServiceRequestID = serviceRequestId };
            return await _dbContext.QuerySingleOrDefaultAsync<ServiceRequest>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ServiceRequest> UpdateAsync(ServiceRequest serviceRequest)
        {
            var sql = "UpdateServiceRequest"; // Name of the stored procedure
            var parameters = new
            {
                serviceRequest.ServiceRequestID,
                serviceRequest.TechnicianID,
                serviceRequest.Description,
                serviceRequest.ServiceType,
                serviceRequest.Status,
                serviceRequest.Priority,
                serviceRequest.EstimatedCompletionTime,
                serviceRequest.ActualCompletionTime,
                serviceRequest.Location
            };
            await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
            return serviceRequest;
        }

        public async Task<bool> DeleteAsync(int serviceRequestId)
        {
            var sql = "DeleteServiceRequest"; // Name of the stored procedure
            var parameters = new { ServiceRequestID = serviceRequestId };
            var affectedRows = await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
            return affectedRows > 0;
        }
    }
}