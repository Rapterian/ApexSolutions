namespace ApexSolutions.DTOs
{
    public class ServiceRequestDTO
    {
        // Properties corresponding to the ServiceRequest model
        public int RequestID { get; set; }
        public int ClientID { get; set; } // To link to the client
        public string Description { get; set; }
        public string PriorityLevel { get; set; }
        public string Status { get; set; }
        public int? AssignedTechnicianID { get; set; } // Nullable if no technician is assigned
        public DateTime CreationTimestamp { get; set; }
        public DateTime? ResolutionTimestamp { get; set; }
    }
}
