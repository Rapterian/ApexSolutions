using ApexCare.Repositories;
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
            if (technician == null || !technician.IsAvailable())
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
            var availableTechnicians = technicians.FindAll(t => t.IsAvailable() && t.Skills.Contains(serviceRequest.PriorityLevel)); // Example logic

            return availableTechnicians;
        }
        public readonly HttpClient _client;
         private async Task SendSmsNotification(string technicianId, string technicianName, string phoneNumber, int requestId, int serviceRequestId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.d7networks.com/messages/v1/send");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJhdXRoLWJhY2tlbmQ6YXBwIiwic3ViIjoiMmNhMTg" +
                "1NmUtOTY2Ny00NWFhLWFkNjEtMGU0ODI1ZTBkY2M4In0.aA0s" +
                "8ATFhxJAy8Hkc463a76sWfWflIUKJWOxKO4PpGM"}");

            var payload = $@"
                {{
                ""messages"": [
                    {{
                        ""channel"": ""sms"",
                        ""recipients"": [""{phoneNumber}""],
                        ""content"": ""{$"Hi {technicianName} TechNo:{technicianId}, a new job has been assigned to you: Request #{requestId}."}"",
                        ""msg_type"": ""text"",
                        ""data_coding"": ""text""
                    }}
                ],
                ""message_globals"": {{
                    ""originator"": ""SignOTP"",
                    ""report_url"": ""https://the_url_to_recieve_delivery_report.com""
                }}
                }}";
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        request.Content = content;
        
        // Send the request and ensure success
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        // Output the response for logging/debugging
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        }
    }
}
