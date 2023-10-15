using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Housing21UI.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Full Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email Address is invalid")]
        [DisplayName("Email Address")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone Number is required")]
        [RegularExpression(@"^\+\d{10}$", ErrorMessage = "Telephone Number must start with a + followed by 10 digits")]
        [DisplayName("Telephone Number")]
        [StringLength(11)]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public int PostPerson(IConfiguration _config)
        {
            string connectionString = _config.GetConnectionString("UserInformationDB");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("pins_Person", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Parameters
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@TelephoneNumber", TelephoneNumber);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);


                connection.Open();

                // return number of rows affected
                // currently stills throws an error if stored proc fails
                return command.ExecuteNonQuery();
            }
        }
    }
}
