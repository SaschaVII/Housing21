using Housing21UI.DataAccess;
using Housing21UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Housing21UI.Pages
{
    public class PersonFormModel : PageModel
    {
        private readonly IDbContext _dbContext;
        private readonly IConfiguration _config;

        [BindProperty]
        public PersonModel Person { get; set; }
        
        public PersonFormModel(IDbContext dbContext)
        {
            _dbContext = dbContext;
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
            Person.PostPerson(_dbContext);

            // Redirect to People Page
            return RedirectToPage("People");

        }
    }
}
