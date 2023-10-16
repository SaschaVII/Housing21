using Housing21UI.Models;

namespace Housing21UI.DataAccess
{
    public interface IDbContext
    {
        int AddPerson(PersonModel person);
        List<PersonModel> GetPeople();
    }
}