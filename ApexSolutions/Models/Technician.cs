using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSolutions.Models
{
    public class Technician
    {
        // Fields
        private int technicianID;
        private string name;
        private string skills;
        private bool availabilityStatus;
        private List<int> assignedRequests;
        private string contactNumber;

        // Properties
        public int TechnicianID { get => technicianID; set => technicianID = value; }
        public string Name { get => name; set => name = value; }
        public string Skills { get => skills; set => skills = value; }
        public bool AvailabilityStatus { get => availabilityStatus; set => availabilityStatus = value; }
        public List<int> AssignedRequests { get => assignedRequests; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }

        // Constructor
        public Technician(int technicianID, string name, string skills, string contactNumber)
        {
            this.technicianID = technicianID;
            this.name = name;
            this.skills = skills;
            availabilityStatus = true; // default to available
            assignedRequests = new List<int>();
            this.contactNumber = contactNumber;
        }

        // Methods
        public void UpdateAvailability(bool isAvailable)
        {
            availabilityStatus = isAvailable;
        }

        public void ReceiveJob(int requestID)
        {
            assignedRequests.Add(requestID);
        }
        public bool IsAvailable()
        {
            return availabilityStatus;
        }

    }
}
