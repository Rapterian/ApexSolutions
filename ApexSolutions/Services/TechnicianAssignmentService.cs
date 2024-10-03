using ApexCare.Repositories;
using ApexSolutions.Interfaces;
using ApexSolutions.Models;
using ApexSolutions.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexSolutions.Services
{
    public class TechnicianAssignmentService
    {
        private readonly ITechnicianRepository _technicianRepository; // Assume you have a technician repository
        private readonly IServiceRequestRepository _serviceRequestRepository;

        public TechnicianAssignmentService(ITechnicianRepository technicianRepository, IServiceRequestRepository serviceRequestRepository)
        {
            _technicianRepository = technicianRepository;
            _serviceRequestRepository = serviceRequestRepository;
        }

        // Assign a technician to a service request
        public async Task<bool> AssignTechnician(int serviceRequestId, int technicianId)
        {
            // Retrieve the service request
            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(serviceRequestId);
            if (serviceRequest == null)
            {
                return false; // Service request not found
            }

            // Retrieve the technician
            var technician = await _technicianRepository.GetByIdAsync(technicianId);
            if (technician == null || !technician.IsAvailable)
            {
                return false; // Technician not found or not available
            }

            // Assign technician
            serviceRequest.AssignedTechnicianID = technicianId;
            serviceRequest.Status = "Assigned"; // Update status of the service request

            // Save changes to the service request
            await _serviceRequestRepository.UpdateAsync(serviceRequest);
            return true;
        }

        // Get available technicians for a specific service request
        public async Task<List<Technician>> GetAvailableTechniciansForServiceRequest(int serviceRequestId)
        {
            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(serviceRequestId);
            if (serviceRequest == null)
            {
                return new List<Technician>(); // Return an empty list if the request is not found
            }

            // Retrieve all technicians
            var technicians = await _technicianRepository.GetAllAsync();

            // Filter available technicians (this could be more complex based on your business logic)
            var availableTechnicians = technicians.FindAll(t => t.IsAvailable && t.Skills.Contains(serviceRequest.PriorityLevel)); // Example logic

            return availableTechnicians;
        }
    }
}
