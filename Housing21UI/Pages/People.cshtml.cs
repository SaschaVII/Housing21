using Housing21UI.DataAccess;
using Housing21UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Housing21UI.Pages
{
    public class PeopleModel : PageModel
    {
        private readonly IDbContext _dbContext;

        public List<PersonModel> People { get; set; }

        public PeopleModel(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnBack()
        {
            return RedirectToPage("PersonForm");
        }

        public void OnGet()
        {
            GetPeople();
        }

        public IActionResult OnPost()
        {
            GetPeople();
            return ExportToCSV();
        }

        private void GetPeople()
        {
            People = _dbContext.GetPeople();
        }

        public IActionResult ExportToCSV()
        {
            // Generate CSV File
            var csv = new StringBuilder();
            csv.AppendLine("Name,Email,TelephoneNumber,DateOfBirth"); // CSV header row

            foreach (var person in People)
            {
                csv.AppendLine($"{person.Name},{person.Email},{person.TelephoneNumber}, {person.DateOfBirth.ToString("d")}");
            }

            byte[] csvBytes = Encoding.UTF8.GetBytes(csv.ToString());

            // Return the CSV File as an IActionResult
            return File(csvBytes, "text/csv", "data.csv");
        }
    }
}
