using System.Data.SqlClient;

namespace Business
{
    public interface IUser
    {
        public bool CheckLogin(string mail, string password);
        public User GetUserWithLogin(string mail, string password);
        public User GetUserWithId(int UserId);
        public int GetHuurderId(int UserId);
        public int GetVerhuurderId(int UserId);

    }
}