using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ApexSolutions.Models;
using ApexSolutions.Interfaces;
using ApexSolutions.Data;

namespace ApexSolutions.Repositories
{
    public class TechnicianRepository : ITechnicianRepository
    {
        private readonly DatabaseContext _dbContext;

        public TechnicianRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
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
            var id = await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
            technician.TechnicianID = id;
            return technician;
        }

        // Get all technicians
        public async Task<IEnumerable<Technician>> GetAllAsync()
        {
            var sql = "GetTechnicians"; // Name of the stored procedure
            return await _dbContext.QueryAsync<Technician>(sql, commandType: CommandType.StoredProcedure);
        }

        // Get a technician by ID
        public async Task<Technician> GetByIdAsync(int id)
        {
            var sql = "GetTechnicianById";
            var parameters = new { TechnicianID = id };
            return await _dbContext.QuerySingleOrDefaultAsync<Technician>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        // Update an existing technician
        public async Task<Technician> UpdateAsync(Technician technician)
        {
            var sql = "UpdateTechnician"; // Name of the stored procedure
            var parameters = new
            {
                technician.TechnicianID,
                technician.Name,
                technician.Skills,
                technician.AvailabilityStatus,
                technician.AssignedRequestIDs
            };
            await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
            return technician;
        }

        // Delete a technician by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DeleteTechnician"; // Name of the stored procedure
            var parameters = new { TechnicianID = id };
            var affectedRows = await _dbContext.ExecuteScalarAsync(sql, parameters, CommandType.StoredProcedure);
            return affectedRows > 0;
        }
    }
}