using Business;
using MotoHut2._0;
using MotoHut2._0.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class MotorDal : IMotorDal
    {
        private readonly string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi391688;User Id=dbi391688;Password=Ikd2N)E105;";


        public void RentMotorDal(int motorId, DateTime ophaal, DateTime inlever)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("INSERT INTO HuurderMotor (MotorId,OphaalDatum,InleverDatum) VALUES(@MotorId,@OphaalDatum,@InleverDatum)", con);
                cmd.Parameters.AddWithValue("@MotorId", motorId);
                cmd.Parameters.AddWithValue("@OphaalDatum", ophaal);
                cmd.Parameters.AddWithValue("@InleverDatum", inlever);
                con.Open();
                cmd.ExecuteNonQuery();
                
            }
        }
        
        public Motor GetMotor(int motorId)
        {
            Motor motor;
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM Motor WHERE MotorId=@MotorId", con);
                cmd.Parameters.AddWithValue("@MotorId", motorId);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                
                if (sdr.Read())
                {
                    motor = new Motor {MotorId = (int)sdr["MotorId"], VerhuurderId = 1, Model = (string)sdr["Model"], Bouwjaar = (int)sdr["Bouwjaar"], Prijs = (int)sdr["Prijs"], Huurbaar = (bool)sdr["Huurbaar"] };
                }
                else
                {
                    motor = null;
                }
            }
            return motor;
        }


    }
}



