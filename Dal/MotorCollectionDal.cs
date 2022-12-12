using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using MotoHut2._0;
using MotoHut2._0.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class MotorCollectionDal : IMotorCollectionDal
    {
        private readonly string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi391688;User Id=dbi391688;Password=Ikd2N)E105;";

        public List<Motor> GetMotorList()
        {
            List<Motor> controlList = new List<Motor>();
            using (var con = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand("SELECT MotorId,Model,Bouwjaar,Prijs,Huurbaar FROM Motor", con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Motor motor = new Motor { MotorId = (int)rdr["MotorId"], VerhuurderId = 1, Model = (string)rdr["Model"], Bouwjaar = (int)rdr["Bouwjaar"], Prijs = (int)rdr["Prijs"], Huurbaar = (bool)rdr["Huurbaar"] };

                        controlList.Add(motor);
                    }
                }
                return controlList;
            }
        }

        public void AddMotor(string merk, int bouwjaar, int prijs, bool huurbaar, int verhuurderId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("INSERT INTO Motor (VerhuurderId, Model, Bouwjaar, Prijs, Huurbaar) VALUES(@VerhuurderId,@Model,@Bouwjaar,@Prijs,@huurbaar)", con);
                cmd.Parameters.AddWithValue("@huurbaar", huurbaar);
                cmd.Parameters.AddWithValue("@Model", merk);
                cmd.Parameters.AddWithValue("@Bouwjaar", bouwjaar);
                cmd.Parameters.AddWithValue("@Prijs", prijs);
                cmd.Parameters.AddWithValue("@VerhuurderId", verhuurderId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMotor(int motorId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("DELETE FROM Motor WHERE MotorId=@MotorId", con);
                cmd.Parameters.AddWithValue("@MotorId", motorId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}



