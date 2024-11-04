using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ApexSolutions.Models;
using ApexCare.Interfaces;
using ApexSolutions.Repositories;

namespace ApexCare.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly IDbConnection _dbConnection;

        public ServiceRequestRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Create a new service request and return the new ServiceRequest object with its ID
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
            var id = await _dbConnection.QuerySingleAsync<int>(sql, parameters, commandType: CommandType.StoredProcedure);
            serviceRequest.ServiceRequestID = id; // Assuming ServiceRequest has a ServiceRequestID property
            return serviceRequest;
        }

        // Get all service requests
        public async Task<IEnumerable<ServiceRequest>> GetAllAsync()
        {
            var sql = "GetServiceRequests"; // Name of the stored procedure
            return await _dbConnection.QueryAsync<ServiceRequest>(sql, commandType: CommandType.StoredProcedure);
        }

        // Get a service request by ID
        public async Task<ServiceRequest> GetByIdAsync(int serviceRequestId)
        {
            var sql = "GetServiceRequestById"; // Assuming you have a stored procedure for this
            var parameters = new { ServiceRequestID = serviceRequestId }; // Assuming the parameter is ServiceRequestID
            return await _dbConnection.QuerySingleOrDefaultAsync<ServiceRequest>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        // Update an existing service request
        public async Task<ServiceRequest> UpdateAsync(ServiceRequest serviceRequest)
        {
            var sql = "UpdateServiceRequest"; // Name of the stored procedure
            var parameters = new
            {
                serviceRequest.ServiceRequestID, // Assuming ServiceRequest has a ServiceRequestID property
                serviceRequest.TechnicianID,
                serviceRequest.Description,
                serviceRequest.ServiceType,
                serviceRequest.Status,
                serviceRequest.Priority,
                serviceRequest.EstimatedCompletionTime,
                serviceRequest.ActualCompletionTime, // Assuming this property exists in your ServiceRequest model
                serviceRequest.Location
            };
            await _dbConnection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            return serviceRequest;
        }

        // Delete a service request
        public async Task<bool> DeleteAsync(int serviceRequestId)
        {
            var sql = "DeleteServiceRequest"; // Name of the stored procedure
            var parameters = new { ServiceRequestID = serviceRequestId }; // Assuming the parameter is ServiceRequestID
            var affectedRows = await _dbConnection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            return affectedRows > 0;
        }
    }
}