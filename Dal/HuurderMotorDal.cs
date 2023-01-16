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
