using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UserCollection : IUserCollection
    {
        public UserCollection() { }


        private readonly IUserCollectionDal _userCollectionDal;


        public UserCollection(IUserCollectionDal i)
        {
            _userCollectionDal = i;
        }

        public void AddUser(string naam, string email, string password, DateTime geboortedatum)
        {
            _userCollectionDal.AddUser(naam, email, password, geboortedatum);
        }
        public bool CheckIfEmailExists(string mail)
        {
            return _userCollectionDal.CheckIfEmailExists(mail);
        }
        public void DeleteUser(int userId)
        {
            _userCollectionDal.DeleteUser(userId);
        }

    }
}
