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
            HuurderMotorCollectionDal _huurderMotorCollectionDal = new HuurderMotorCollectionDal();
            List<HuurderMotor> controlList = _huurderMotorCollectionDal.GetHuurderMotorListForMotor(motorId);

            if (controlList.Count > 0)
            {
                foreach (var item in controlList)
                {
                    return !(ophaal <= item.InleverDatum && inlever >= item.OphaalDatum && item.IsGeaccepteerd == true);
                }
            }
            return true;
        }

        public void AcceptOrDeclineRent(int huurderMotorId, bool AcceptRent)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("UPDATE HuurderMotor SET IsGeaccepteerd=@Accept WHERE HuurderMotorId=@HuurderMotorId", con);             
                cmd.Parameters.AddWithValue("@Accept", AcceptRent);
                cmd.Parameters.AddWithValue("@HuurderMotorId", huurderMotorId);
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }

        
    }
}
