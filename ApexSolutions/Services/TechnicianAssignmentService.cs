using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexSolutions.Interfaces;
using ApexSolutions.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TechnicianAssignmentService> _logger;

        public TechnicianAssignmentService(
            ITechnicianRepository technicianRepository,
            IServiceRequestRepository serviceRequestRepository,
            ISmsService smsService,
            ILogger<TechnicianAssignmentService> logger)
        {
            _technicianRepository = technicianRepository;
            _serviceRequestRepository = serviceRequestRepository;
            _smsService = smsService;
            _logger = logger;
        }

        // Assign a technician to a service request
        public async Task<bool> AssignTechnicianAsync(int serviceRequestId, int technicianId)
        {
            try
            {
                // Retrieve the service request
                var serviceRequest = await _serviceRequestRepository.GetByIdAsync(serviceRequestId);
                if (serviceRequest == null)
                {
                    _logger.LogWarning($"Service Request ID {serviceRequestId} not found.");
                    return false; // Service request not found
                }

                // Retrieve the technician
                var technician = await _technicianRepository.GetByIdAsync(technicianId);
                if (technician == null || !technician.IsAvailable())
                {
                    _logger.LogWarning($"Technician ID {technicianId} not found or unavailable.");
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
            catch (Exception ex)
            {
                // Log the error with relevant details
                _logger.LogError(ex, $"Error assigning Technician ID {technicianId} to Service Request ID {serviceRequestId}");
                return false;
            }
        }

        // Get available technicians for a specific service request
        public async Task<List<Technician>> GetAvailableTechniciansForServiceRequestAsync(int serviceRequestId)
        {
            try
            {
                var serviceRequest = await _serviceRequestRepository.GetByIdAsync(serviceRequestId);
                if (serviceRequest == null)
                {
                    _logger.LogWarning($"Service Request ID {serviceRequestId} not found.");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving available technicians for Service Request ID {serviceRequestId}");
                return new List<Technician>(); // Return an empty list if an error occurs
            }
        }


        // Notify technician via SMS
        private async Task NotifyTechnicianAsync(string contactNumber, int serviceRequestId)
        {
            try
            {
                var message = $"You have been assigned a new job. Job ID: {serviceRequestId}";
                await _smsService.SendSmsAsync("your_api_token", contactNumber, "", message);
                _logger.LogInformation($"SMS sent to Technician with Contact Number {contactNumber} for Service Request ID {serviceRequestId}");
            }
            catch (Exception ex)
            {
                // Log SMS sending failure, but do not fail the entire assignment process
                _logger.LogError(ex, $"Failed to send SMS to Technician with Contact Number {contactNumber} for Service Request ID {serviceRequestId}");
            }
        }

            var message = $"You have been assigned a new job. Job ID: {serviceRequestId}";
            await _smsService.SendSmsAsync("your_api_token", contactNumber, "", message);


    }
}
