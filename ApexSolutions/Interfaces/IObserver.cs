using ApexSolutions.Models;

namespace ApexSolutions.Interfaces
{
    public interface IObserver
    {
        // Method to update the observer with changes
        void Update(ServiceRequest serviceRequest);

        // Optional: Method to handle removal from the observer list
        void Remove();
    }
}
