using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApexCare.Data;
using ApexCare.Interfaces;
using ApexSolutions.Models;

namespace ApexCare.Repositories
{
    public class ServiceRequestRepository : IRepository<ServiceRequest>
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new service request
        public async Task<ServiceRequest> AddAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();
            return serviceRequest;
        }

        // Get all service requests
        public async Task<IEnumerable<ServiceRequest>> GetAllAsync()
        {
            return await _context.ServiceRequests.ToListAsync();
        }

        // Get a service request by ID
        public async Task<ServiceRequest> GetByIdAsync(int id)
        {
            return await _context.ServiceRequests.FindAsync(id);
        }

        // Update an existing service request
        public async Task<ServiceRequest> UpdateAsync(ServiceRequest serviceRequest)
        {
            _context.Entry(serviceRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return serviceRequest;
        }

        // Delete a service request
        public async Task<bool> DeleteAsync(int id)
        {
            var serviceRequest = await GetByIdAsync(id);
            if (serviceRequest == null)
            {
                return false;
            }

            _context.ServiceRequests.Remove(serviceRequest);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
