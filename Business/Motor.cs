using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Motor : IMotor
    {
        public Motor() { }
        
        public int MotorId { get; set; }
        public int VerhuurderId { get; set; }
        public string Model { get; set; }
        public int Bouwjaar { get; set; }
        public int Prijs { get; set; }
        public bool Huurbaar { get; set; }
        
        

        private readonly IMotorDal _motorDal;

        public Motor(IMotorDal i)
        {
            _motorDal = i;
        }



        public void RentMotor(int motorId, DateTime ophaal, DateTime inlever, int prijs, int huurderId)
        {
            _motorDal.RentMotorDal(motorId, ophaal, inlever, prijs, huurderId);
        }

        public Motor GetMotor(int motorId)
        {
            return _motorDal.GetMotor(motorId);
        }
        public void EditMotor(string merk, int bouwjaar, int prijs, bool huurbaar, int motorId)
        {
            _motorDal.EditMotor(merk, bouwjaar, prijs, huurbaar, motorId);
        }
    }
}
