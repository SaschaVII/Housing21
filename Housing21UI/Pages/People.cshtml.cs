using Housing21UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace Housing21UI.Pages
{
    public class PeopleModel : PageModel
    {
        private readonly IConfiguration _config;

        public List<PersonModel> People { get; set; }

        public PeopleModel(IConfiguration config)
        {
            _config = config;
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
            People = new List<PersonModel>();
            string connectionString = _config.GetConnectionString("UserInformationDB");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("psel_GetPersons", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PersonModel person = new PersonModel();
                        person.Name = reader.GetString("Name");
                        person.Email = reader.GetString("Email");
                        person.TelephoneNumber = reader.GetString("TelephoneNumber");
                        person.DateOfBirth = reader.GetDateTime("DateOfBirth");

                        People.Add(person);
                    }
                }
            }
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
