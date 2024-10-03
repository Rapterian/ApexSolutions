using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSolutions
{
    public class Client
    {
        // Fields
        private int clientID;
        private string name;
        private string contactDetails;
        private List<ServiceRequest> serviceHistory;
        private int contractID;
        private bool isKeyClient;

        // Properties
        public int ClientID { get => clientID; set => clientID = value; }
        public string Name { get => name; set => name = value; }
        public string ContactDetails { get => contactDetails; set => contactDetails = value; }
        public List<ServiceRequest> ServiceHistory { get => serviceHistory; set => serviceHistory = value; }
        public int ContractID { get => contractID; set => contractID = value; }
        public bool IsKeyClient { get => isKeyClient; set => isKeyClient = value; }

        // Constructor
        public Client(int clientID, string name, string contactDetails, int contractID)
        {
            this.clientID = clientID;
            this.name = name;
            this.contactDetails = contactDetails;
            this.contractID = contractID;
            this.serviceHistory = new List<ServiceRequest>();
        }

        // Methods
        public void LogClientInteraction(ServiceRequest request)
        {
            serviceHistory.Add(request);
        }

        public void FlagKeyClient()
        {
            this.isKeyClient = true;
        }
    }
}
