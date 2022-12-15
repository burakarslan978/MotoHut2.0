using System.Data.SqlClient;

namespace Business
{
    public interface IUserDal
    {
        //public bool CheckLogin(string mail, string password);
        public User GetHashedPasswordAndUserId(string mail);
        public User GetUserWithLogin(string mail);
        public int GetUserIdWithLogin(string mail);
        public User GetUserWithId(int UserId);
        public int GetHuurderId(int UserId);
        public int GetVerhuurderId(int UserId);
        public string GetNameWithId(int UserId);
        public void EditUser(string naam, string email, string password, DateTime geboortedatum, int userId);
        public int GetUserIdWithVerhuurderId(int verhuurderId);
        public int GetUserIdWithHuurderId(int huurderId);
        public DateTime GetDoBWithId(int UserId);

    }
}