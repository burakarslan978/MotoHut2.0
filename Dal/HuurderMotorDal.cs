﻿using Business;
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
                        //ControlFactory factory = new ControlFactory();
                        //IMotorCollection motor = factory.CreateControl(i);
                        HuurderMotor huurdermotor = new HuurderMotor { HuurderMotorId = (int)rdr["HuurderMotorId"], MotorId = (int)rdr["MotorId"], HuurderId = 1, OphaalDatum = (DateTime)rdr["OphaalDatum"], InleverDatum = (DateTime)rdr["InleverDatum"] };

                        controlList.Add(huurdermotor);
                    }
                }
                return controlList;
            }
        }
    }
}
