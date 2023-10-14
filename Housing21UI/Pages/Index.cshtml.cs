using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Housing21UI.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Redirect to PersonForm
            return RedirectToPage("/PersonForm");
        }
    }
}