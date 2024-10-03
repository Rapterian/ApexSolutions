using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSolutions.Models
{
    public class ServiceRequest
    {
        // Fields
        private int requestID;
        private int clientID;
        private string issueDetails;
        private string priorityLevel;
        private string status;
        private int assignedTechnicianID;
        private DateTime creationTimestamp;
        private DateTime? resolutionTimestamp;

        // Properties
        public int RequestID { get => requestID; set => requestID = value; }
        public int ClientID { get => clientID; set => clientID = value; }
        public string IssueDetails { get => issueDetails; set => issueDetails = value; }
        public string PriorityLevel { get => priorityLevel; set => priorityLevel = value; }
        public string Status { get => status; set => status = value; }
        public int AssignedTechnicianID { get => assignedTechnicianID; set => assignedTechnicianID = value; }
        public DateTime CreationTimestamp { get => creationTimestamp; }
        public DateTime? ResolutionTimestamp { get => resolutionTimestamp; set => resolutionTimestamp = value; }

        // Constructor
        public ServiceRequest(int requestId, int clientId, string issueDetails, string priorityLevel)
        {
            requestID = requestId;
            clientID = clientId;
            this.issueDetails = issueDetails;
            this.priorityLevel = priorityLevel;
            status = "Open"; // default status
            creationTimestamp = DateTime.Now;
        }

        // Methods (Stubs)
        public void AssignTechnician(int technicianID)
        {
            assignedTechnicianID = technicianID;
            status = "Assigned";
        }

        public void CloseRequest()
        {
            status = "Closed";
            resolutionTimestamp = DateTime.Now;
        }

        public void EscalateRequest()
        {
            priorityLevel = "High";
            status = "Escalated";
        }
    }
}
