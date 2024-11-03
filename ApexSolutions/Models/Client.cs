using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApexSolutions.Models
{
    [Table("Client")] // Specify the table name
    public class Client
    {
        // Properties
        [Key] // Marks this property as the primary key
        public int ClientID { get; set; }

        [Required] // Indicates that this field is required
        [MaxLength(100)] // Specifies a maximum length for the string
        public string Name { get; set; }

        [Required] // Indicates that this field is required
        [MaxLength(100)] // Specifies a maximum length for the string
        [EmailAddress] // Validates that the string is a valid email address
        public string Email { get; set; }

        [Required] // Indicates that this field is required
        [MaxLength(15)] // Specifies a maximum length for the string
        public string PhoneNumber { get; set; }

        [MaxLength(255)] // Specifies a maximum length for the string
        public string Address { get; set; }

        // Navigation property for related service requests
        public virtual ICollection<ServiceRequest> ServiceHistory { get; set; } = new List<ServiceRequest>();

        // Optional: You can add additional properties as needed
        public bool IsKeyClient { get; set; } = false; // Default value

        // Parameterless constructor for EF or other frameworks
        public Client() { }

        // Constructor
        public Client(string name, string email, string phoneNumber, string address)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            ServiceHistory = new List<ServiceRequest>(); // Initialize the service history
        }

        // Methods
        public void LogClientInteraction(ServiceRequest request)
        {
            ServiceHistory.Add(request);
        }

        public void FlagKeyClient()
        {
            IsKeyClient = true;
        }
    }
}