using Microsoft.AspNetCore.Mvc.RazorPages;
using ApexSolutions.DTOs; // Make sure to include the DTO namespace
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApexUI.Pages
{
    public class ServiceRequestsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IEnumerable<ServiceRequestDTO> ServiceRequests { get; set; }

        public ServiceRequestsModel(IHttpClientFactory httpClientFactory)
        {
            // Use the named HttpClient
            _httpClient = httpClientFactory.CreateClient("ServiceRequestAPI");
        }

        public async Task OnGetAsync()
        {
            ServiceRequests = await _httpClient.GetFromJsonAsync<IEnumerable<ServiceRequestDTO>>("api/ServiceRequest");
        }
    }
}