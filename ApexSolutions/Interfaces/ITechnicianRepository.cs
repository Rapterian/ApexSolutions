using System.Collections.Generic;
using System.Threading.Tasks;
using ApexSolutions.Models;

namespace ApexSolutions.Repositories
{
    public interface ITechnicianRepository
    {
        // Add a new technician
        Task<Technician> AddAsync(Technician technician);

        // Get all technicians
        Task<IEnumerable<Technician>> GetAllAsync();

        // Get a technician by ID
        Task<Technician> GetByIdAsync(int id);

        // Update an existing technician
        Task<Technician> UpdateAsync(Technician technician);

        // Delete a technician by ID
        Task<bool> DeleteAsync(int id);
    }
}