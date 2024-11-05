using ApexSolutions.DTOs;
using ApexSolutions.Services;
using Microsoft.AspNetCore.Mvc;
using ApexSolutions.Interfaces;

namespace ApexUI.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        public IActionResult Index()
        {
            var requests = _serviceRequestService.GetAllServiceRequestsAsync();
            return View(requests);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceRequestDTO dto)
        {
            if (ModelState.IsValid)
            {
                _serviceRequestService.CreateServiceRequestAsync(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        public IActionResult Edit(int id)
        {
            var request = _serviceRequestService.GetServiceRequestByIdAsync(id);
            if (request == null) return NotFound();
            return View(request);
        }
    }

}
