using Housing21UI.DataAccess;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        public int PostPerson(IDbContext dbContext)
        {
            return dbContext.AddPerson(this);
        }
    }
}
