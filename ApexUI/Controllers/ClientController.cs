using Microsoft.AspNetCore.Mvc;

namespace ApexUI.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult ClientWelcome()
        {
            return View("ClientWelcome");
        }

        public IActionResult ClientLogin()
        {
            return View("ClientLogin");
        }

        public IActionResult ClientRegister()
        {
            return View("ClientRegister");
        }

        public IActionResult ClientFeedback()
        {
            return View("ClientFeedback");
        }

        public IActionResult ClientDashboard()
        {
            return View("ClientDashboard");
        }

        public IActionResult ClientServices()
        {
            return View("ClientServices");
        }
    }
}
