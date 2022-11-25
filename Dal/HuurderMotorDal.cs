using Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class HuurderMotorDal : IHuurderMotorDal
    {
        private readonly string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi391688;User Id=dbi391688;Password=Ikd2N)E105;";

        public bool CheckAvailability(int motorId, DateTime ophaal, DateTime inlever) //niet beschikbaar = false
        {
            List<HuurderMotor> controlList = new List<HuurderMotor>();
            using (var con = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand("SELECT * FROM HuurderMotor WHERE MotorId=@MotorID AND IsGeaccepteerd=1", con))
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
            }
            if (controlList.Count > 0)
            {
                foreach (var item in controlList)
                {
                    if (ophaal <= item.InleverDatum && inlever >= item.OphaalDatum)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
                return true;
        }

        public void AcceptOrDeclineRent(int huurderMotorId, string acceptOrDecline)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                string _decision = "";
                if (acceptOrDecline == "Accept")
                {
                    _decision = "IsGeaccepteerd";
                }
                else if (acceptOrDecline == "Decline")
                {
                    _decision = "IsGeweigerd";
                }
                var cmd = new SqlCommand("UPDATE HuurderMotor SET "+_decision+"=1 WHERE HuurderMotorId=@HuurderMotorId", con);             
                cmd.Parameters.AddWithValue("@HuurderMotorId", huurderMotorId);
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }
    }
}
