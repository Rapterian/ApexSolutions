using System;

namespace ApexSolutions.DTOs
{
    public class ServiceRequestDTO
    {
        public int ServiceRequestID { get; set; } // Unique identifier for the service request
        public int? ClientID { get; set; } // Foreign key referencing the client
        public string Description { get; set; } // Description of the service request
        public string ServiceType { get; set; } // Type of service requested
        public DateTime RequestDate { get; set; } // Date when the request was made
        public string Status { get; set; } // Current status of the service request (e.g., Open, In Progress, Closed)
        public string PriorityLevel { get; set; } // Priority level of the request (e.g., Low, Medium, High)

        // public int? AssignedTechnicianID { get; set; } // This is optional, only if we want the ID of the technician assigned to the request
        // public DateTime? ResolutionTimestamp { get; set; } // This is optional, only if we want the timestamp for when the request was resolved
    }
}