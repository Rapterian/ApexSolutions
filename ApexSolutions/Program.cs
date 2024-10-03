using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSolutions
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Create a client
            Client client = new Client(1, "John Doe", "johndoe@example.com", 1001);

            // Create a service request
            ServiceRequest request = new ServiceRequest(101, client.ClientID, "Air conditioner malfunction", "Medium");

            // Assign a technician to the service request
            Technician technician = new Technician(501, "Jane Smith", "HVAC Specialist");
            technician.ReceiveJob(request.RequestID);
            request.AssignTechnician(technician.TechnicianID);

            // Log client interaction and service request
            client.LogClientInteraction(request);

            // Capture client feedback
            Feedback feedback = new Feedback(1, client.ClientID, request.RequestID, 5, "Great service!");
            feedback.RecordFeedback(5, "The technician was very professional and quick.");

        }
    }
}