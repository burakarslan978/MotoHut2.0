using System.Data.SqlClient;

namespace Business
{
    public interface IUserDal
    {
        public bool CheckLogin(string mail, string password);
        public User GetUserWithLogin(string mail, string password);
        public int GetUserIdWithLogin(string mail, string password);
        public User GetUserWithId(int UserId);
        public int GetHuurderId(int UserId);
        public int GetVerhuurderId(int UserId);
        public string GetNameWithId(int UserId);
        public void AddUser(string naam, string email, string password, DateTime geboortedatum);
    }
}