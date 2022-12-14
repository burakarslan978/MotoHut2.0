using System.Data.SqlClient;

namespace Business
{
    public interface IUserCollection
    {
        public void AddUser(string naam, string email, string password, DateTime geboortedatum);
        public bool CheckIfEmailExists(string mail);
    }
}