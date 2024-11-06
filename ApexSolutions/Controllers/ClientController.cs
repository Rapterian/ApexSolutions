using Microsoft.AspNetCore.Mvc;
using ApexSolutions.DTOs; // Assuming you have a DTO for Client
using ApexSolutions.Services; // Assuming the ClientService is in this namespace

namespace ApexSolutions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        // Inject the ClientService through constructor injection
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        // Get a list of all clients (GET request)
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        // Get a specific client by ID (GET request)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();  // Return 404 if the client is not found
            }
            return Ok(client);
        }

        // Create a new client (POST request)
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDTO clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return 400 if the data is invalid
            }

            var createdClient = await _clientService.CreateClientAsync(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.ClientID }, createdClient);
        }

        // Update an existing client (PUT request)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDTO clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return 400 if the data is invalid
            }

            var updatedClient = await _clientService.UpdateClientAsync(id, clientDto);
            if (updatedClient == null)
            {
                return NotFound();  // Return 404 if the client doesn't exist
            }

            return NoContent();  // Return 204 if the update was successful
        }

        // Delete a client (DELETE request)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _clientService.DeleteClientAsync(id);
            if (!result)
            {
                return NotFound();  // Return 404 if the client is not found
            }
            return NoContent();  // Return 204 if the delete was successful
        }
    }
}