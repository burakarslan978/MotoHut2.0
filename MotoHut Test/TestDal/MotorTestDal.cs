using Business;
using MotoHut2._0;
using MotoHut2._0.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace HuurderMotorCollectionTest.TestDal
{
    public class MotorTestDal : IMotorDal
    {     
        public void RentMotorDal(int motorId, DateTime ophaal, DateTime inlever, int prijs, int huurderId)
        {          
        }
        
        public Motor GetMotor(int motorId)
        {
            Motor motor = new Motor();
            motor.Model = "Yamaha";
            return motor;
        }

        public void EditMotor(string merk, int bouwjaar, int prijs, bool huurbaar, int motorId)
        {

        }


    }
}



