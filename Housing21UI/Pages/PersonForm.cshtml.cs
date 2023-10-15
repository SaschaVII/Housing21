using Housing21UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Housing21UI.Pages
{
    public class PersonFormModel : PageModel
    {
        [BindProperty]
        public PersonModel Person { get; set; }
        
        private readonly IConfiguration _config;

        public PersonFormModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Process the form data and perform validation.
            if (!ModelState.IsValid)
            {
                // Reload page and show invalid inputs
                return Page();
            }

            // Post data to DB
            Person.PostPerson(_config);

            // Redirect to People Page
            return RedirectToPage("People");

        }
    }
}
