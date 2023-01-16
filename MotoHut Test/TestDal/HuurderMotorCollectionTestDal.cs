using Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuurderMotorCollectionTest.TestDal
{

    public class HuurderMotorCollectionTestDal : IHuurderMotorCollectionDal
    {

        public List<HuurderMotor> GetHuurderMotorList()
        {
            List<HuurderMotor> controlList = new List<HuurderMotor>();

            return controlList;

        }

        public List<HuurderMotor> GetHuurderMotorListForMotor(int motorId)
        {
            var controlList = new List<HuurderMotor> {
        new HuurderMotor { HuurderMotorId = 1, OphaalDatum = new DateTime(2022, 1, 2), InleverDatum = new DateTime(2022, 1, 5), IsGeaccepteerd = null },
        new HuurderMotor { HuurderMotorId = 2, OphaalDatum = new DateTime(2022, 1, 10), InleverDatum = new DateTime(2022, 1, 20), IsGeaccepteerd = null },
        new HuurderMotor { HuurderMotorId = 3, OphaalDatum = new DateTime(2022, 1, 16), InleverDatum = new DateTime(2022, 1, 20), IsGeaccepteerd = null },
        new HuurderMotor { HuurderMotorId = 4, OphaalDatum = new DateTime(2022, 2, 2), InleverDatum = new DateTime(2022, 2, 5), IsGeaccepteerd = true },
        new HuurderMotor { HuurderMotorId = 5, OphaalDatum = new DateTime(2022, 2, 7), InleverDatum = new DateTime(2022, 2, 8), IsGeaccepteerd = false },
        new HuurderMotor { HuurderMotorId = 6, OphaalDatum = new DateTime(2022, 2, 8), InleverDatum = new DateTime(2022, 2, 9), IsGeaccepteerd = null }
    };
            return controlList;

        }



        private bool? CheckIfBoolColumnIsNull(object obj)
        {
            if (obj == System.DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(obj);
            }

        }

        public void DeleteHuurderMotorForMotor(int motorId)
        {

        }

        public void DeleteHuurderMotorForHuurder(int huurderId)
        {

        }
    }
}
