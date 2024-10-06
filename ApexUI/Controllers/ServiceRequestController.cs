using ApexSolutions.DTOs;
using ApexSolutions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApexUI.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestRepository _serviceRequestService;

        public ServiceRequestController(IServiceRequestRepository serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        public IActionResult Index()
        {
            var requests = _serviceRequestService.GetAllAsync();
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
                _serviceRequestService.CreateServiceRequest(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        public IActionResult Edit(int id)
        {
            var request = _serviceRequestService.GetServiceRequestById(id);
            if (request == null) return NotFound();
            return View(request);
        }
    }

}
