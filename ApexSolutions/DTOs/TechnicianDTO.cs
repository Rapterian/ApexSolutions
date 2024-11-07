using System.ComponentModel.DataAnnotations;

namespace ApexSolutions.DTOs
{
    public class TechnicianDTO
    {
        public int TechnicianID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Skills are required.")]
        [MaxLength(255, ErrorMessage = "Skills cannot exceed 255 characters.")]
        public string Skills { get; set; }

        [Required(ErrorMessage = "Availability status is required.")]
        public bool AvailabilityStatus { get; set; } = true; // Default to true (available)

        public string AssignedRequestIDs { get; set; }

        [MaxLength(15, ErrorMessage = "Contact number cannot exceed 15 characters.")]
        public string ContactNumber { get; set; }
    }
}