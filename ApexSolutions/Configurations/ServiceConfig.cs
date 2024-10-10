namespace ApexSolutions.Configurations
{
    public class ServiceConfig
    {
        // Configuration for general service request processing
        public int MaxServiceRequestRetries { get; set; } = 3;  // Default value if not specified

        // Thresholds for escalation of service requests
        public int EscalationThresholdInHours { get; set; } = 24;  // Escalate if unresolved within this time

        // Settings related to technician assignment
        public int MaxTechnicianAssignments { get; set; } = 5;  // Maximum number of active requests per technician

        // Feedback and survey settings
        public int FeedbackReminderDelayInDays { get; set; } = 2;  // Delay before sending feedback reminders

        // Any other settings related to contracts, scheduling, etc.
        public bool EnableAutoContractRenewal { get; set; } = true;  // Automatic contract renewals

        public string DefaultServiceLevel { get; set; } = "Standard";  // Default service level for requests
    }
}
