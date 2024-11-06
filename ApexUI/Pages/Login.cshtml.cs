using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApexUI.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            // Handle GET request
        }

        public IActionResult OnPost(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (IsValidUser(username, password))
            {
                return RedirectToPage("TechnicianDashboard");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page(); // Return to the same page to show the error
            }
        }

        private bool IsValidUser(string username, string password)
        {
            // Replace this with your actual validation logic
            return username == "admin" && password == "password"; // Example
        }
    }
}