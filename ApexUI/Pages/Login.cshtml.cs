using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace ApexUI.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            // Handle GET request
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (IsClient(Username, Password))
            {
                // Redirect to client dashboard if user is a client
                return RedirectToPage("ClientDashboard");
            }
            else if (IsTechnician(Username, Password))
            {
                // Redirect to technician dashboard if user is a technician
                return RedirectToPage("TechnicianDashboard");
            }
            else
            {
                // Show error if login is invalid
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        private bool IsClient(string username, string password)
        {
            return CheckCredentials("Data/Clients.txt", username, password);
        }

        private bool IsTechnician(string username, string password)
        {
            return CheckCredentials("Data/Technicians.txt", username, password);
        }

        private bool CheckCredentials(string filePath, string username, string password)
        {
            // Read all lines from the file
            string[] lines = System.IO.File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var credentials = line.Split(',');
                if (credentials.Length == 2 &&
                    credentials[0] == username &&
                    credentials[1] == password)
                {
                    return true; // Valid credentials found
                }
            }
            return false; // No valid credentials found
        }
    }
}
