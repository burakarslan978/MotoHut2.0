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
        public static string ConnectServer()
        {
            string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi391688;User Id=dbi391688;Password=Ikd2N)E105;";

            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sql = "SELECT * from Motor";
            SqlDataAdapter ad = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            string motor = "";

            foreach (DataRow dr in dt.Rows)
            {
                motor += dr["MotorId"].ToString();
                motor += dr["Model"].ToString();
                motor += dr["Bouwjaar"].ToString();
                motor += dr["Prijs"].ToString();

            }

            conn.Close();

            return motor;
        }

        public List<Motor> MotorControl()
        {
            List<Motor> controlList = new List<Motor>();
            using (var con = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand("SELECT * FROM Motor", con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //ControlFactory factory = new ControlFactory();
                        //IMotorCollection motor = factory.CreateControl(i);
                        Motor motor = new Motor { MotorId = (int)rdr["MotorId"], VerhuurderId = 1, Model = (string)rdr["Model"], Bouwjaar = (int)rdr["Bouwjaar"], Prijs = (int)rdr["Prijs"], Huurbaar = (bool)rdr["Huurbaar"] };

                        controlList.Add(motor);
                    }
                }
                return controlList;
            }
        }

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

        public void AddMotor(string merk, int bouwjaar, int prijs)
        {
            using(var con = new SqlConnection(connectionstring))
            {
                var cmd = new SqlCommand("INSERT INTO Motor (Model, Bouwjaar, Prijs, Status) VALUES(@Model,@Bouwjaar,@Prijs,'Vrij')", con);
                cmd.Parameters.AddWithValue("@Model", merk);
                cmd.Parameters.AddWithValue("@Bouwjaar", bouwjaar);
                cmd.Parameters.AddWithValue("@Prijs", prijs);
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

        //public object query(string query)
        //{

        //}

        //public class ControlFactory
        //{
        //    public IMotorCollection CreateControl(IMotorCollection control)
        //    {
        //        //if (control is Motor)
        //        //    return new Motor();
        //        //else if (control is )
        //        //    return new ();
        //        //else
        //            return new MotorCollection();
        //    }
        //}

    }
}



