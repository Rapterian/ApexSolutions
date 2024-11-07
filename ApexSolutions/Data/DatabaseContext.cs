using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexSolutions.Models;
using System.Data;


namespace ApexSolutions.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> ExecuteScalarAsync(string sql, object parameters = null, CommandType? commandType = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: commandType);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, CommandType? commandType = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sql, parameters, commandType: commandType);
            }
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null, CommandType? commandType = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters, commandType: commandType);
            }
        }

        // Client Methods
        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Client>("GetClients", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new { ClientID = clientId };
                return await connection.QuerySingleOrDefaultAsync<Client>("GetClientById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> InsertClientAsync(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Check if the client already exists
                if (await ClientExistsAsync(client.Email))
                {
                    // Update existing client
                    var updateParameters = new
                    {
                        client.ClientID,
                        client.Name,
                        client.Email,
                        client.PhoneNumber,
                        client.Address
                    };

                    await connection.ExecuteAsync("UpdateClient", updateParameters, commandType: System.Data.CommandType.StoredProcedure);

                    var existingClientId = await connection.QuerySingleAsync<int>(
                        "SELECT ClientID FROM Client WHERE Email = @Email", new { Email = client.Email });
                    return existingClientId; // Return the existing ClientID
                }
                else
                {
                    // Insert the new client
                    var insertParameters = new
                    {
                        client.Name,
                        client.Email,
                        client.PhoneNumber,
                        client.Address
                    };

                    // Execute the stored procedure and return the new ClientID
                    return await connection.ExecuteScalarAsync<int>("InsertClient", insertParameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
        }

        public async Task<bool> ClientExistsAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new { Email = email };

                // Query to check if a client with the given email already exists
                var existingClient = await connection.QuerySingleOrDefaultAsync<int?>(
                    "SELECT ClientID FROM Client WHERE Email = @Email", parameters);

                return existingClient.HasValue; // Returns true if a client exists
            }
        }
        // Technician Methods
        public async Task<IEnumerable<Technician>> GetTechniciansAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Technician>("GetTechnicians", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> InsertTechnicianAsync(Technician technician)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    technician.Name,
                    technician.Skills,
                    technician.AvailabilityStatus,
                    technician.AssignedRequestIDs
                };

                // Execute the stored procedure and return the new TechnicianID
                return await connection.ExecuteScalarAsync<int>("InsertTechnician", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        // Service Request Methods
        public async Task<IEnumerable<ServiceRequest>> GetServiceRequestsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<ServiceRequest>("GetServiceRequests", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> InsertServiceRequestAsync(ServiceRequest serviceRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    serviceRequest.ClientID,
                    serviceRequest.TechnicianID,
                    serviceRequest.Description,
                    serviceRequest.ServiceType,
                    serviceRequest.RequestDate,
                    serviceRequest.Status,
                    serviceRequest.Priority,
                    serviceRequest.EstimatedCompletionTime,
                    serviceRequest.Location
                };

                // Execute the stored procedure and return the new ServiceRequestID
                return await connection.ExecuteScalarAsync<int>("InsertServiceRequest", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> InsertFeedbackAsync(Feedback feedback)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    feedback.ServiceRequestID,
                    feedback.ClientID,
                    feedback.Rating, 
                    feedback.Comments,
                    feedback.SubmittedDate 
                };

                // Execute the stored procedure and return the new FeedbackID
                return await connection.ExecuteScalarAsync<int>("InsertFeedback", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
