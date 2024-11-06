using System.ComponentModel.DataAnnotations;

namespace ApexSolutions.DTOs
{
    public class ClientDTO
    {
        // Assuming ClientID is an auto-generated property
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(255, ErrorMessage = "Email cannot be longer than 255 characters.")]
        public string Email { get; set; }

        // Add other properties as necessary
        // For example:
        [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        public string PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "Adress cannot be longer than 100 characters.")]
        public string Address { get; set; }

        // Additional properties can be added here
    }
}