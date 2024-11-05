using ApexSolutions.Interfaces;
using ApexSolutions.Models;
using ApexSolutions.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace ApexSolutions.Services
{
    public class TechnicianAssignmentService
    {
        private readonly ITechnicianRepository _technicianRepository;
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly ISmsService _smsService;

        public TechnicianAssignmentService(
            ITechnicianRepository technicianRepository,
            IServiceRequestRepository serviceRequestRepository,
            ISmsService smsService)
        {
            _technicianRepository = technicianRepository;
            _serviceRequestRepository = serviceRequestRepository;
            _smsService = smsService;
        }

        // Assign a technician to a service request
        public async Task<bool> AssignTechnicianAsync(int serviceRequestId, int technicianId)
        {
            // Retrieve the service request
            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(serviceRequestId);
            if (serviceRequest == null)
            {
                return false; // Service request not found
            }

            // Retrieve the technician
            var technician = await _technicianRepository.GetByIdAsync(technicianId);
            if (technician == null || !technician.IsAvailable())
            {
                return false; // Technician not found or not available
            }

            // Assign technician
            serviceRequest.TechnicianID = technicianId;
            serviceRequest.Status = "Assigned"; // Update status of the service request

            // Save changes to the service request
            await _serviceRequestRepository.UpdateAsync(serviceRequest);

            // Send SMS notification to technician
            await NotifyTechnicianAsync(technician.ContactNumber, serviceRequestId);

            return true;
        }

        // Get available technicians for a specific service request
        public async Task<List<Technician>> GetAvailableTechniciansForServiceRequestAsync(int serviceRequestId)
        {
            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(serviceRequestId);
            if (serviceRequest == null)
            {
                return new List<Technician>(); // Return an empty list if the request is not found
            }

            // Retrieve all technicians
            var technicians = await _technicianRepository.GetAllAsync();

            // Filter available technicians based on business logic
            var availableTechnicians = technicians
                .Where(t => t.IsAvailable() && t.Skills.Contains(serviceRequest.Priority)) // Example logic
                .ToList();

            return availableTechnicians;
        }


        // Notify technician via SMS
        private async Task NotifyTechnicianAsync(string contactNumber, int serviceRequestId)
        {
            var message = $"You have been assigned a new job. Job ID: {serviceRequestId}";
            await _smsService.SendSmsAsync("your_api_token", contactNumber, "", message);

    }
}