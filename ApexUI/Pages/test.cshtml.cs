using Microsoft.AspNetCore.Mvc.RazorPages;
using ApexSolutions.DTOs; // Make sure to include the DTO namespace
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApexUI.Pages
{
    public class ClientsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IEnumerable<ClientDTO> Clients { get; set; } // Change to IEnumerable

        public ClientsModel(IHttpClientFactory httpClientFactory)
        {
            // Use the named HttpClient
            _httpClient = httpClientFactory.CreateClient("ClientAPI");
        }

        public async Task OnGetAsync()
        {
            Clients = await _httpClient.GetFromJsonAsync<IEnumerable<ClientDTO>>("api/client"); // This will now work
        }
    }
}