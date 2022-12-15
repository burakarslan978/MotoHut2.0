using System.Data.SqlClient;

namespace Business
{
    public interface IUserCollectionDal
    {
        public void AddUser(string naam, string email, string password, DateTime geboortedatum);
        public bool CheckIfEmailExists(string mail);
        public void DeleteUser(int userId, int huurderId, int verhuurderId);
    }
}