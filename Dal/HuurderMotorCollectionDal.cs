using Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class HuurderMotorCollectionDal : IHuurderMotorCollectionDal
    {
        private readonly string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi391688;User Id=dbi391688;Password=Ikd2N)E105;";

        public List<HuurderMotor> GetHuurderMotorList()
        {
            List<HuurderMotor> controlList = new List<HuurderMotor>();
            using (var con = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand("SELECT HuurderMotorId,HuurderId,MotorId,OphaalDatum,InleverDatum,IsGeaccepteerd FROM HuurderMotor", con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        HuurderMotor huurdermotor = new HuurderMotor { HuurderMotorId = (int)rdr["HuurderMotorId"], MotorId = (int)rdr["MotorId"], HuurderId = (int)rdr["HuurderId"], OphaalDatum = (DateTime)rdr["OphaalDatum"], InleverDatum = (DateTime)rdr["InleverDatum"], IsGeaccepteerd = CheckIfBoolColumnIsNull(rdr["IsGeaccepteerd"]), Prijs = (int)rdr["Prijs"] };

                        controlList.Add(huurdermotor);
                    }
                }
                return controlList;
            }
        }

        public List<HuurderMotor> GetHuurderMotorListForMotor(int motorId)
        {
            List<HuurderMotor> controlList = new List<HuurderMotor>();
            using (var con = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand("SELECT HuurderMotorId,HuurderId,MotorId,OphaalDatum,InleverDatum,IsGeaccepteerd,Prijs FROM HuurderMotor WHERE MotorId=@MotorID", con))
                {
                    cmd.Parameters.AddWithValue("@MotorId", motorId);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        HuurderMotor huurdermotor = new HuurderMotor { HuurderMotorId = (int)rdr["HuurderMotorId"], MotorId = (int)rdr["MotorId"], HuurderId = (int)rdr["HuurderId"], OphaalDatum = (DateTime)rdr["OphaalDatum"], InleverDatum = (DateTime)rdr["InleverDatum"], IsGeaccepteerd = CheckIfBoolColumnIsNull(rdr["IsGeaccepteerd"]), Prijs = (int)rdr["Prijs"] };

                        controlList.Add(huurdermotor);
                    }
                }
                return controlList;
            }
        }

        private bool? CheckIfBoolColumnIsNull(object obj)
        {
            if(obj == System.DBNull.Value)
            {
                return null;
            } else
            {
                return Convert.ToBoolean(obj);
            }
            
        }
        
        public void DeleteHuurderMotorForMotor(int motorId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("DELETE FROM HuurderMotor WHERE MotorId=@MotorId", con);
                cmd.Parameters.AddWithValue("@MotorId", motorId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteHuurderMotorForHuurder(int huurderId)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("DELETE FROM HuurderMotor WHERE HuurderId=@huurderId", con);
                cmd.Parameters.AddWithValue("@huurderId", huurderId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }




    }
}
