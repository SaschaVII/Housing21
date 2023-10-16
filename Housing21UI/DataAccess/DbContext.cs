using Housing21UI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Housing21UI.DataAccess
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _config;

        public DbContext(IConfiguration config)
        {
            _config = config;
        }

        public int AddPerson(PersonModel person)
        {
            try
            {
                // Post data to DB
                string connectionString = _config.GetConnectionString("UserInformationDB");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("pins_Person", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Email", person.Email);
                    command.Parameters.AddWithValue("@TelephoneNumber", person.TelephoneNumber);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);


                    connection.Open();

                    // return number of rows affected
                    // currently stills throws an error if stored proc fails
                    return command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception("An error occurred when attempting to upload data to the Database.");
            }
        }

        public List<PersonModel> GetPeople()
        {
            try
            {
                // Get data from DB
                var result = new List<PersonModel>();
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

                            result.Add(person);
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw new Exception("An error occurred when attempting to fetch data from the Database.");
            }
        }
    }
}
