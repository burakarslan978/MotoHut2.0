using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class User : IUser
    {
        public User() { }

        public int UserId { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Leeftijd { get; set; }



        private readonly IUserDal _userDal;

        public User(IUserDal i)
        {
            _userDal = i;
        }
        public bool CheckLogin(string mail, string password)
        {
            return _userDal.CheckLogin(mail, password);
        }
        public User GetUserWithLogin(string mail, string password)
        {
            return _userDal.GetUserWithLogin(mail, password);
        }
        public User GetUserWithId(int UserId)
        {
            return _userDal.GetUserWithId(UserId);
        }
        public int GetHuurderId(int UserId)
        {
            return _userDal.GetHuurderId(UserId);
        }
        public int GetVerhuurderId(int UserId)
        {
            return _userDal.GetVerhuurderId(UserId);
        }


    }
}
