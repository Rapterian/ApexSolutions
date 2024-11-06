using Microsoft.AspNetCore.Mvc.RazorPages;
using ApexSolutions.Services; // Adjust the namespace as needed
using ApexSolutions.DTOs; // Make sure to include the DTO namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexUI.Pages
{
    public class ClientsModel : PageModel
    {
        private readonly ClientService _clientService;

        public IEnumerable<ClientDTO> Clients { get; set; } // Change to IEnumerable

        public ClientsModel(ClientService clientService)
        {
            _clientService = clientService;
        }


        public async Task OnGetAsync()
        {
            Clients = await _clientService.GetAllClientsAsync(); // Fetch clients on GET request
        }
    }
}