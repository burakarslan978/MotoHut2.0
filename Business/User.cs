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
        public string? Naam { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime Geboortedatum { get; set; }



        private readonly IUserDal _userDal;


        public User(IUserDal i)
        {
            _userDal = i;
        }
        //public bool CheckLogin(string mail, string password)
        //{
        //    return _userDal.CheckLogin(mail, password);
        //}
        public User GetHashedPasswordAndUserId(string mail)
        {
            return _userDal.GetHashedPasswordAndUserId(mail);
        }
        public User GetUserWithLogin(string mail)
        {
            return _userDal.GetUserWithLogin(mail);
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
        public int GetUserIdWithLogin(string mail)
        {
            return _userDal.GetUserIdWithLogin(mail);
        }

        public string GetNameWithId(int UserId)
        {
            return _userDal.GetNameWithId(UserId);
        }
        public void EditUser(string naam, string email, string password, DateTime geboortedatum, int userId)
        {
            _userDal.EditUser(naam, email, password, geboortedatum, userId);
        }
        public int GetUserIdWithVerhuurderId(int verhuurderId)
        {
            return _userDal.GetUserIdWithVerhuurderId(verhuurderId);
        }
        public int GetUserIdWithHuurderId(int huurderId)
        {
            return _userDal.GetUserIdWithHuurderId(huurderId);
        }
        public int GetAgeWithId(int UserId)
        {
            return (DateTime.Now.Subtract(_userDal.GetDoBWithId(UserId)).Days / 365);
        }


    }
}
