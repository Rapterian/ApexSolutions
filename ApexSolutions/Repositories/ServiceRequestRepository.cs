using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApexCare.Data;
using ApexCare.Interfaces;
using ApexSolutions.Models;
using ApexSolutions.Repositories;

namespace ApexCare.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository // Implement IServiceRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new service request
        async Task<ServiceRequest> IServiceRequestRepository.AddAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();
            return serviceRequest;
        }

        // Get all service requests
        async Task<List<ServiceRequest>> IServiceRequestRepository.GetAllAsync()
        {
            return await _context.ServiceRequests.ToListAsync();
        }


        // Get a service request by ID
        async Task<ServiceRequest> IServiceRequestRepository.GetByIdAsync(int serviceRequestId) // Match method signature
        {
            return await _context.ServiceRequests.FindAsync(serviceRequestId);
        }

        // Update an existing service request
        async Task<ServiceRequest> IServiceRequestRepository.UpdateAsync(ServiceRequest serviceRequest)
        {
            _context.Entry(serviceRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return serviceRequest;
        }


        // Delete a service request
        async Task<bool> IServiceRequestRepository.DeleteAsync(int serviceRequestId)
        {
            // Call the interface implementation
            var serviceRequest = await ((IServiceRequestRepository)this).GetByIdAsync(serviceRequestId);
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
