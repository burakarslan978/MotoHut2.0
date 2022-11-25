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
                using (var cmd = new SqlCommand("SELECT * FROM HuurderMotor", con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        HuurderMotor huurdermotor = new HuurderMotor { HuurderMotorId = (int)rdr["HuurderMotorId"], MotorId = (int)rdr["MotorId"], HuurderId = 1, OphaalDatum = (DateTime)rdr["OphaalDatum"], InleverDatum = (DateTime)rdr["InleverDatum"], IsGeaccepteerd = (bool)rdr["IsGeaccepteerd"], IsGeweigerd = (bool)rdr["IsGeweigerd"] };

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
                using (var cmd = new SqlCommand("SELECT * FROM HuurderMotor WHERE MotorId=@MotorID", con))
                {
                    cmd.Parameters.AddWithValue("@MotorId", motorId);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        HuurderMotor huurdermotor = new HuurderMotor { HuurderMotorId = (int)rdr["HuurderMotorId"], MotorId = (int)rdr["MotorId"], HuurderId = 1, OphaalDatum = (DateTime)rdr["OphaalDatum"], InleverDatum = (DateTime)rdr["InleverDatum"], IsGeaccepteerd = (bool)rdr["IsGeaccepteerd"], IsGeweigerd = (bool)rdr["IsGeweigerd"] };

                        controlList.Add(huurdermotor);
                    }
                }
                return controlList;
            }
        }

        
    }
}
