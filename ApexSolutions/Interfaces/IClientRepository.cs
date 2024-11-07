using System.Collections.Generic;
using System.Threading.Tasks;
using ApexSolutions.Models;

namespace ApexSolutions.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync(); // Retrieve all clients
        Task<Client> GetByIdAsync(int id); // Retrieve a client by ID
        Task<Client> AddAsync(Client client); // Add a new client
        Task<Client> UpdateAsync(Client client); // Update an existing client
        Task DeleteAsync(Client client); // Delete a client
    }
}