using ApexCare.Data;
using ApexSolutions.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexSolutions.Repositories
{
    public class TechnicianRepository : ITechnicianRepository
    {
        private readonly ApplicationDbContext _context;

        public TechnicianRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Technician> GetByIdAsync(int technicianId)
        {
            return await _context.Technicians.FindAsync(technicianId);
        }

        public async Task<List<Technician>> GetAllAsync()
        {
            return await _context.Technicians.ToListAsync();
        }

        public async Task AddAsync(Technician technician)
        {
            await _context.Technicians.AddAsync(technician);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Technician technician)
        {
            _context.Technicians.Update(technician);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int technicianId)
        {
            var technician = await GetByIdAsync(technicianId);
            if (technician != null)
            {
                _context.Technicians.Remove(technician);
                await _context.SaveChangesAsync();
            }
        }
    }
}
