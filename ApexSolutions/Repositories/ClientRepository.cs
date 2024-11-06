using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ApexSolutions.Models;
using ApexSolutions.Interfaces;
using ApexSolutions.Data;
using Microsoft.EntityFrameworkCore;

namespace ApexSolutions.Repositories
{
    public class ClientRepository : IClientRepository // Change here to implement IClientRepository
    {
        private readonly DatabaseContext _dbContext;

        public ClientRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create a new client and return the new Client object with its ID
        public async Task<Client> AddAsync(Client client)
        {
            var sql = "InsertClient"; // Name of the stored procedure
            var parameters = new
            {
                client.Name,
                client.Email,
                client.PhoneNumber, // Assuming this property exists in your Client model
                client.Address
            };
            var id = await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
            client.ClientID = id; // Assuming Client has a ClientID property
            return client;
        }

        // Get all clients
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            var sql = "GetClients"; // Name of the stored procedure
            return await _dbContext.QueryAsync<Client>(sql, commandType: CommandType.StoredProcedure);
        }

        // Get a client by ID
        public async Task<Client> GetByIdAsync(int id)
        {
            var sql = "GetClientById"; // Assuming you have a stored procedure for this
            var parameters = new { ClientID = id }; // Assuming the parameter is ClientID
            return await _dbContext.QuerySingleOrDefaultAsync<Client>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        // Update an existing client
        public async Task<Client> UpdateAsync(Client client)
        {
            var sql = "UpdateClient"; // Name of the stored procedure
            var parameters = new
            {
                client.ClientID, // Assuming Client has a ClientID property
                client.Name,
                client.Email,
                client.PhoneNumber,
                client.Address
            };
            await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
            return client;
        }

        // Delete a client
        public async Task DeleteAsync(Client client) // Change to match the interface method signature
        {
            var sql = "DeleteClient"; // Name of the stored procedure
            var parameters = new { ClientID = client.ClientID }; // Assuming the parameter is ClientID
            await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
        }
    }
}