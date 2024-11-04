using System.Collections.Generic;
using System.Threading.Tasks;
using ApexSolutions.DTOs;

namespace ApexSolutions.Services
{
    public interface ITechnicianService
    {
        Task<IEnumerable<TechnicianDTO>> GetAllTechniciansAsync();
        Task<TechnicianDTO> GetTechnicianByIdAsync(int id);
        Task<TechnicianDTO> CreateTechnicianAsync(TechnicianDTO technicianDto);
        Task<TechnicianDTO> UpdateTechnicianAsync(int id, TechnicianDTO technicianDto);
        Task<bool> DeleteTechnicianAsync(int id);
    }
}