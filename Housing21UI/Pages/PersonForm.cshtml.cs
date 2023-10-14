using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Housing21UI.Pages
{
    public class PersonFormModel : PageModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? TelephoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Process the form data and perform validation.
            if (ModelState.IsValid)
            {
                // If the data is valid, perform further actions, e.g., save to a database.
                // You can also show a success message.
            }
            else
            {
                // If there are validation errors, show them to the user.
                // Return to the same page to display the form with errors.
            }

            return Page();
        }
    }
}
