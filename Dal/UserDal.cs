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
                var cmd = new SqlCommand("SELECT UserId,Password FROM [User] WHERE Email=@email", con);
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

        public void EditUser(string naam, string email, string password, DateTime geboortedatum, int userId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("UPDATE [User] SET Naam=@Naam, Email=@Email, Password=@Password, Geboortedatum=@Geboortedatum" +
                    " WHERE UserId=@UserId", con);
                cmd.Parameters.AddWithValue("@Naam", naam);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Geboortedatum", geboortedatum);
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserWithLogin(string mail)
        {
            User user;
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT UserId,Email,GeboorteDatum,Naam FROM [User] WHERE Email=@email", con);
                cmd.Parameters.AddWithValue("@email", mail);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return user = new User { UserId = (int)sdr["UserId"], Email = (string)sdr["Email"],
                        Geboortedatum = (DateTime)sdr["Geboortedatum"], Naam = (string)sdr["Naam"] };
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
                var cmd = new SqlCommand("SELECT UserId,Email,GeboorteDatum,Naam FROM [User] WHERE UserId=@userId", con);
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

        public DateTime GetDoBWithId(int UserId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT Geboortedatum FROM [User] WHERE UserId=@userId", con);
                cmd.Parameters.AddWithValue("@userId", UserId);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return (DateTime)sdr["Geboortedatum"];
                }
                else
                {
                    return DateTime.Now;
                }
            }

        }

        public int GetHuurderId(int UserId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT HuurderId FROM Huurder WHERE UserId=@userId", con);
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
                var cmd = new SqlCommand("SELECT VerhuurderId FROM Verhuurder WHERE UserId=@userId", con);
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

        public int GetUserIdWithVerhuurderId(int verhuurderId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT UserId FROM Verhuurder WHERE VerhuurderId=@verhuurderId", con);
                cmd.Parameters.AddWithValue("@verhuurderId", verhuurderId);
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

        public int GetUserIdWithHuurderId(int huurderId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT UserId FROM Huurder WHERE HuurderId=@huurderId", con);
                cmd.Parameters.AddWithValue("@huurderId", huurderId);
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


    }
}
