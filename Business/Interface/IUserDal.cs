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
        
    }
}