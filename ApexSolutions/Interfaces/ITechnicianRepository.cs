using ApexSolutions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexSolutions.Repositories
{
    public interface ITechnicianRepository
    {
        Task<Technician> GetByIdAsync(int technicianId);
        Task<List<Technician>> GetAllAsync();
        Task AddAsync(Technician technician);
        Task UpdateAsync(Technician technician);
        Task DeleteAsync(int technicianId);
    }
}
