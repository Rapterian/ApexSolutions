using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ApexSolutions.Models;
using ApexSolutions.Interfaces;

namespace ApexSolutions.Repositories
{
    public class TechnicianRepository : IRepository<Technician>
    {
        private readonly IDbConnection _dbConnection;

        public TechnicianRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Create a new technician and return the new Technician object with its ID
        public async Task<Technician> AddAsync(Technician technician)
        {
            var sql = "InsertTechnician"; // Name of the stored procedure
            var parameters = new
            {
                technician.Name,
                technician.Skills,
                technician.AvailabilityStatus,
                technician.AssignedRequestIDs
            };
            var id = await _dbConnection.QuerySingleAsync<int>(sql, parameters, commandType: CommandType.StoredProcedure);
            technician.TechnicianID = id; // Assuming Technician has a TechnicianID property
            return technician;
        }

        // Get all technicians
        public async Task<IEnumerable<Technician>> GetAllAsync()
        {
            var sql = "GetTechnicians"; // Name of the stored procedure
            return await _dbConnection.QueryAsync<Technician>(sql, commandType: CommandType.StoredProcedure);
        }

        // Get a technician by ID
        public async Task<Technician> GetByIdAsync(int id)
        {
            var sql = "GetTechnicianById"; // Assuming you have a stored procedure for this
            var parameters = new { TechnicianID = id }; // Assuming the parameter is TechnicianID
            return await _dbConnection.QuerySingleOrDefaultAsync<Technician>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        // Update an existing technician
        public async Task<Technician> UpdateAsync(Technician technician)
        {
            var sql = "UpdateTechnician"; // Name of the stored procedure
            var parameters = new
            {
                technician.TechnicianID, // Assuming Technician has a TechnicianID property
                technician.Name,
                technician.Skills,
                technician.AvailabilityStatus,
                technician.AssignedRequestIDs
            };
            await _dbConnection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            return technician;
        }

        // Delete a technician
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DeleteTechnician"; // Name of the stored procedure
            var parameters = new { TechnicianID = id }; // Assuming the parameter is TechnicianID
            var affectedRows = await _dbConnection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            return affectedRows > 0;
        }
    }
}