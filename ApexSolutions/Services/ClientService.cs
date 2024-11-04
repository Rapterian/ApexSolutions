using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using ApexSolutions.DTOs; 
using ApexSolutions.Models;
using ApexSolutions.Repositories;
using ApexSolutions.Interfaces;

namespace ApexSolutions.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        // Constructor injection of the repository
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // Get all clients
        public async Task<IEnumerable<ClientDTO>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            // Map the Client model to ClientDTO
            return MapToDTO(clients);
        }

        // Get a specific client by ID
        public async Task<ClientDTO> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return client != null ? MapToDTO(client) : null;
        }

        // Create a new client
        public async Task<ClientDTO> CreateClientAsync(ClientDTO clientDto)
        {
            var client = MapToModel(clientDto);
            var createdClient = await _clientRepository.AddAsync(client);
            return MapToDTO(createdClient);
        }

        // Update an existing client
        public async Task<ClientDTO> UpdateClientAsync(int id, ClientDTO clientDto)
        {
            var existingClient = await _clientRepository.GetByIdAsync(id);
            if (existingClient == null)
            {
                return null; // Client not found
            }

            existingClient.Name = clientDto.Name; // Example property
            existingClient.Email = clientDto.Email; // Example property

            var updatedClient = await _clientRepository.UpdateAsync(existingClient);
            return MapToDTO(updatedClient);
        }

        // Delete a client
        public async Task<bool> DeleteClientAsync(int id)
        {
            var existingClient = await _clientRepository.GetByIdAsync(id);
            if (existingClient == null)
            {
                return false; // Client not found
            }

            await _clientRepository.DeleteAsync(existingClient);
            return true;
        }

        // Mapping methods
        private ClientDTO MapToDTO(Client client)
        {
            return new ClientDTO
            {
                ClientID = client.ClientID,
                Name = client.Name,
                Email = client.Email,
                // Map other properties as necessary
            };
        }

        private IEnumerable<ClientDTO> MapToDTO(IEnumerable<Client> clients)
        {
            return clients.Select(client => MapToDTO(client)); // Map each Client to ClientDTO
        }

        private Client MapToModel(ClientDTO clientDto)
        {
            return new Client
            {
                ClientID = clientDto.ClientID,
                Name = clientDto.Name,
                Email = clientDto.Email,
                // Map other properties as necessary
            };
        }
    }
}