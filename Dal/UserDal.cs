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

        public bool CheckLogin(string mail, string password)
        {
            
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM [User] WHERE Email=@email AND Password=@password", con);
                cmd.Parameters.AddWithValue("@email", mail);
                cmd.Parameters.AddWithValue("@password", password);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public User GetUserWithLogin(string mail, string password)
        {
            User user;
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM [User] WHERE Email=@email AND Password=@password", con);
                cmd.Parameters.AddWithValue("@email", mail);
                cmd.Parameters.AddWithValue("@password", password);
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

        public int GetUserIdWithLogin(string mail, string password)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT UserId FROM [User] WHERE Email=@email AND Password=@password", con);
                cmd.Parameters.AddWithValue("@email", mail);
                cmd.Parameters.AddWithValue("@password", password);
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

        public void AddUser(string naam, string email, string password, DateTime geboortedatum)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("INSERT INTO [User] (Naam, Email, Password, Geboortedatum) VALUES(@Naam,@Email,@Password,@Geboortedatum)", con);
                cmd.Parameters.AddWithValue("@Naam", naam);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Geboortedatum", geboortedatum);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            int newUserId = GetUserIdWithLogin(email, password);
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("INSERT INTO Huurder (UserId) VALUES(@UserId)", con);
                var cmd2 = new SqlCommand("INSERT INTO Verhuurder (UserId) VALUES(@UserId)", con);
                cmd.Parameters.AddWithValue("@UserId", newUserId);
                cmd2.Parameters.AddWithValue("@UserId", newUserId);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }
        }

    }
}
