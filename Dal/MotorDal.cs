﻿using Business;
using MotoHut2._0;
using MotoHut2._0.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Dal
{
    public class MotorDal : IMotorDal
    {
        private readonly string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi391688;User Id=dbi391688;Password=Ikd2N)E105;";


        public void RentMotorDal(int motorId, DateTime ophaal, DateTime inlever, int prijs, int huurderId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("INSERT INTO HuurderMotor (MotorId,HuurderId,OphaalDatum,InleverDatum,Prijs,IsGeaccepteerd) VALUES(@MotorId,@HuurderId,@OphaalDatum,@InleverDatum,@Prijs,NULL)", con);
                cmd.Parameters.AddWithValue("@MotorId", motorId);
                cmd.Parameters.AddWithValue("@OphaalDatum", ophaal);
                cmd.Parameters.AddWithValue("@InleverDatum", inlever);
                cmd.Parameters.AddWithValue("@Prijs", prijs);
                cmd.Parameters.AddWithValue("@HuurderId", huurderId);
                con.Open();
                cmd.ExecuteNonQuery();
                
            }
        }
        
        public Motor GetMotor(int motorId)
        {
            Motor motor;
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("SELECT MotorId,VerhuurderId,Model,Bouwjaar,Prijs,Huurbaar FROM Motor WHERE MotorId=@MotorId", con);
                cmd.Parameters.AddWithValue("@MotorId", motorId);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                
                if (sdr.Read())
                {
                    motor = new Motor {MotorId = (int)sdr["MotorId"], VerhuurderId = (int)sdr["VerhuurderId"], Model = (string)sdr["Model"], Bouwjaar = (int)sdr["Bouwjaar"], Prijs = (int)sdr["Prijs"], Huurbaar = (bool)sdr["Huurbaar"] };
                }
                else
                {
                    motor = null;
                }
            }
            return motor;
        }

        public void EditMotor(string merk, int bouwjaar, int prijs, bool huurbaar, int motorId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                try
                {
                    var cmd = new SqlCommand("UPDATE Motor SET Model=@Model, Bouwjaar=@Bouwjaar, Prijs=@Prijs, Huurbaar=@huurbaar" +
                    " WHERE MotorId=@MotorId", con);
                    cmd.Parameters.AddWithValue("@huurbaar", huurbaar);
                    cmd.Parameters.AddWithValue("@Model", merk);
                    cmd.Parameters.AddWithValue("@Bouwjaar", bouwjaar);
                    cmd.Parameters.AddWithValue("@Prijs", prijs);
                    cmd.Parameters.AddWithValue("@MotorId", motorId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.ToString());
                }

        }
        }


    }
}



