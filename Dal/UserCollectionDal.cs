using Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class UserCollectionDal : IUserCollectionDal
    {
        private readonly string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi391688;User Id=dbi391688;Password=Ikd2N)E105;";


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
            UserDal userdal = new UserDal();
            int newUserId = userdal.GetUserIdWithLogin(email);
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

        public void DeleteUser(int userId, int huurderId, int verhuurderId)
        {
            MotorCollectionDal motorCollectionDal = new MotorCollectionDal();
            HuurderMotorCollectionDal huurderMotorCollectionDal =  new HuurderMotorCollectionDal();
            huurderMotorCollectionDal.DeleteHuurderMotorForHuurder(huurderId);
            motorCollectionDal.DeleteMotorsForVerhuurder(verhuurderId);
            DeleteHuurder(userId);
            DeleteVerhuurder(userId);
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("DELETE FROM [User] WHERE UserId=@UserId", con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteVerhuurder(int userId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("DELETE FROM Verhuurder WHERE UserId=@UserId", con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteHuurder(int userId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("DELETE FROM Huurder WHERE UserId=@UserId", con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public bool CheckIfEmailExists(string mail)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT Email FROM [User] WHERE Email=@email", con);
                cmd.Parameters.AddWithValue("@email", mail);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                return sdr.Read();
            }
        }

    }
}
