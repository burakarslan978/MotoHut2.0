using Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class UserDal : IUserDal
    {
        private readonly string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi391688;User Id=dbi391688;Password=Ikd2N)E105;";

        public User GetHashedPasswordAndUserId(string mail)
        {
            
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM [User] WHERE Email=@email", con);
                cmd.Parameters.AddWithValue("@email", mail);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return new User { Password = (string)sdr["Password"], UserId = (int)sdr["UserId"] };
                }
                else
                {
                    return null;
                }
            }
        }

        public User GetUserWithLogin(string mail)
        {
            User user;
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM [User] WHERE Email=@email", con);
                cmd.Parameters.AddWithValue("@email", mail);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return user = new User { UserId = (int)sdr["UserId"], Email = (string)sdr["Email"], Geboortedatum = (DateTime)sdr["Geboortedatum"], Naam = (string)sdr["Naam"] };
                }
                else
                {
                    return user = null;
                }
            }
        }

        

        public int GetUserIdWithLogin(string mail)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT UserId FROM [User] WHERE Email=@email", con);
                cmd.Parameters.AddWithValue("@email", mail);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return (int)sdr["UserId"];
                }
                else
                {
                    return 0;
                }
            }
        }

        public User GetUserWithId(int UserId)
        {
            User user;
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM [User] WHERE UserId=@userId", con);
                cmd.Parameters.AddWithValue("@userId", UserId);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return user = new User { UserId = (int)sdr["UserId"], Email = (string)sdr["Email"], Geboortedatum = (DateTime)sdr["Geboortedatum"], Naam = (string)sdr["Naam"] };
                }
                else
                {
                    return user = null;
                }
            }
            
        }

        public string GetNameWithId(int UserId)
        {
            User user;
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT Naam FROM [User] WHERE UserId=@userId", con);
                cmd.Parameters.AddWithValue("@userId", UserId);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return (string)sdr["Naam"];
                }
                else
                {
                    return "-";
                }
            }

        }

        public int GetHuurderId(int UserId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM Huurder WHERE UserId=@userId", con);
                cmd.Parameters.AddWithValue("@userId", UserId);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return (int)sdr["HuurderId"];
                }
                else
                {
                    return 0;
                }
            }
        }

        public int GetVerhuurderId(int UserId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM Verhuurder WHERE UserId=@userId", con);
                cmd.Parameters.AddWithValue("@userId", UserId);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return (int)sdr["VerhuurderId"];
                }
                else
                {
                    return 0;
                }
            }
        }


    }
}
