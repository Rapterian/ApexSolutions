using System;
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
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository), "Client repository cannot be null");
        }

        // Get all clients
        public async Task<IEnumerable<ClientDTO>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync();
                return MapToDTO(clients);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error fetching clients: {ex.Message}");
                return Enumerable.Empty<ClientDTO>();
            }
        }

        // Get a specific client by ID
        public async Task<ClientDTO> GetClientByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid client ID");
            }

            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                return client != null ? MapToDTO(client) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching client with ID {id}: {ex.Message}");
                return null;
            }
        }

        // Create a new client
        public async Task<ClientDTO> CreateClientAsync(ClientDTO clientDto)
        {
            if (clientDto == null)
            {
                throw new ArgumentNullException(nameof(clientDto), "ClientDTO cannot be null");
            }

            try
            {
                var client = MapToModel(clientDto);
                var createdClient = await _clientRepository.AddAsync(client);
                return MapToDTO(createdClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating client: {ex.Message}");
                return null;
            }
        }

        // Update an existing client
        public async Task<ClientDTO> UpdateClientAsync(int id, ClientDTO clientDto)
        {
            if (clientDto == null)
            {
                throw new ArgumentNullException(nameof(clientDto), "ClientDTO cannot be null");
            }

            try
            {
                var existingClient = await _clientRepository.GetByIdAsync(id);
                if (existingClient == null)
                {
                    Console.WriteLine($"Client with ID {id} not found");
                    return null;
                }

                existingClient.Name = clientDto.Name;
                existingClient.Email = clientDto.Email;

                var updatedClient = await _clientRepository.UpdateAsync(existingClient);
                return MapToDTO(updatedClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating client with ID {id}: {ex.Message}");
                return null;
            }
        }

        // Delete a client
        public async Task<bool> DeleteClientAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid client ID");
            }

            try
            {
                var existingClient = await _clientRepository.GetByIdAsync(id);
                if (existingClient == null)
                {
                    Console.WriteLine($"Client with ID {id} not found");
                    return false;
                }

                await _clientRepository.DeleteAsync(existingClient);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting client with ID {id}: {ex.Message}");
                return false;
            }
        }

        // Mapping methods
        private ClientDTO MapToDTO(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "Client cannot be null");
            }

            return new ClientDTO
            {
                ClientID = client.ClientID,
                Email=client.Email,
                Name = client.Name,
                Address = client.Address

            };
        }

        private IEnumerable<ClientDTO> MapToDTO(IEnumerable<Client> clients)
        {
            if (clients == null)
            {
                throw new ArgumentNullException(nameof(clients), "Client list cannot be null");
            }

            return clients.Select(client => MapToDTO(client));
        }

        private Client MapToModel(ClientDTO clientDto)
        {
            if (clientDto == null)
            {
                throw new ArgumentNullException(nameof(clientDto), "ClientDTO cannot be null");
            }

            return new Client
            {
                ClientID = clientDto.ClientID,
                Name = clientDto.Name,
                Email = clientDto.Email,
            };
        }
    }
}
