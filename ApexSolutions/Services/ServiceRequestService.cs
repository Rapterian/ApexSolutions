using ApexSolutions.Models;

namespace ApexSolutions.Services
{
    public class ServiceRequestService
    {
        public ServiceRequest CreateRequest(string type)
        {
            if (type == "Standard")
                return new StandardServiceRequest();
            else if (type == "Escalated")
                return new EscalatedServiceRequest();
            else
                throw new ArgumentException("Invalid request type");
        }
    }
}
