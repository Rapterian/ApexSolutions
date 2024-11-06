using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApexSolutions.Models
{
    [Table("ServiceRequest")] // Specify the table name
    public class ServiceRequest
    {
        // Properties
        [Key] // Marks this property as the primary key
        public int ServiceRequestID { get; set; }

        [ForeignKey("Client")] // Specify the foreign key relationship
        public int? ClientID { get; set; } // Nullable to allow for unassigned requests

        [ForeignKey("Technician")] // Specify the foreign key relationship
        public int? TechnicianID { get; set; } // Nullable to allow for unassigned requests

        [Required] // Indicates that this field is required
        public string Description { get; set; }

        [Required] // Indicates that this field is required
        [MaxLength(50)] // Specifies a maximum length for the string
        public string ServiceType { get; set; }

        [Required] // Indicates that this field is required
        public DateTime RequestDate { get; set; }

        [Required] // Indicates that this field is required
        public string Status { get; set; }

        [Required] // Indicates that this field is required
        public string Priority { get; set; }

        public DateTime? EstimatedCompletionTime { get; set; } // Nullable

        public DateTime? ActualCompletionTime { get; set; } // Nullable

        [Required] // Indicates that this field is required
        public string Location { get; set; }

        public string ClientFeedback { get; set; } // Nullable

        public int? Rating { get; set; } // Nullable

        public string TechnicianFeedback { get; set; } // Nullable

        // Parameterless constructor for EF or other frameworks
        public ServiceRequest() { }

        // Constructor
        public ServiceRequest(int clientId, string description, string serviceType, DateTime requestDate, string status, string priority, string location)
        {
            ClientID = clientId;
            Description = description;
            ServiceType = serviceType;
            RequestDate = requestDate;
            Status = status;
            Priority = priority;
            Location = location;
        }

        // Methods
        public void AssignTechnician(int technicianID)
        {
            TechnicianID = technicianID;
            Status = "Assigned";
        }

        public void CloseRequest()
        {
            Status = "Closed";
            ActualCompletionTime = DateTime.Now; // Set actual completion time when closing
        }

        public void EscalateRequest()
        {
            Priority = "High";
            Status = "Escalated";
        }
    }
}