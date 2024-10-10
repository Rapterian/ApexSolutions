using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSolutions.Models
{
    public class Contract
    {
        // Fields
        private int contractID;
        private int clientID;
        private string servicePackage;
        private string performanceMetrics;
        private string contractStatus;
        private DateTime renewalDate;

        // Properties
        public int ContractID { get => contractID; set => contractID = value; }
        public int ClientID { get => clientID; set => clientID = value; }
        public string ServicePackage { get => servicePackage; set => servicePackage = value; }
        public string PerformanceMetrics { get => performanceMetrics; set => performanceMetrics = value; }
        public string ContractStatus { get => contractStatus; set => contractStatus = value; }
        public DateTime RenewalDate { get => renewalDate; set => renewalDate = value; }

        // Constructor
        public Contract(int contractID, int clientID, string servicePackage, DateTime renewalDate)
        {
            this.contractID = contractID;
            this.clientID = clientID;
            this.servicePackage = servicePackage;
            contractStatus = "Active";
            this.renewalDate = renewalDate;
        }

        // Methods
        public void RenewContract()
        {
            contractStatus = "Renewed";
            renewalDate = DateTime.Now.AddYears(1); // Renew for 1 year
        }

        public void TrackPerformance(string metrics)
        {
            performanceMetrics = metrics;
        }
    }
}
