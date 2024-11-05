using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApexSolutions.Models
{
    [Table("Technician")] // Specify the table name
    public class Technician
    {
        // Properties
        [Key] // Marks this property as the primary key
        public int TechnicianID { get; set; }

        [Required] // Indicates that this field is required
        [MaxLength(100)] // Specifies a maximum length for the string
        public string Name { get; set; }

        [Required] // Indicates that this field is required
        [MaxLength(255)] // Specifies a maximum length for the string
        public string Skills { get; set; }

        [Required] // Indicates that this field is required
        public bool AvailabilityStatus { get; set; } = true; // Default to true (available)

        public string AssignedRequestIDs { get; set; } // This will hold a comma-separated list of request IDs

        // Optional: Add a property for contact number if needed
        public string ContactNumber { get; set; }

        // Parameterless constructor for EF or other frameworks
        public Technician() { }

        // Constructor
        public Technician(int technicianID, string name, string skills, string contactNumber)
        {
            TechnicianID = technicianID;
            Name = name;
            Skills = skills;
            ContactNumber = contactNumber;
            AvailabilityStatus = true; // Default to available
            AssignedRequestIDs = null; // Initialize to null
        }

        // Methods
        public void UpdateAvailability(bool isAvailable)
        {
            AvailabilityStatus = isAvailable;
        }

        public void ReceiveJob(int requestID)
        {
            if (AssignedRequestIDs == null)
            {
                AssignedRequestIDs = requestID.ToString();
            }
            else
            {
                AssignedRequestIDs += $",{requestID}"; // Append new request ID
            }
        }

        public bool IsAvailable()
        {
            return AvailabilityStatus;
        }
    }
}