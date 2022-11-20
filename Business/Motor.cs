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



        public void RentMotor(int motorId, DateTime ophaal, DateTime inlever)
        {
            _motorDal.RentMotorDal(motorId, ophaal, inlever);
        }

        public Motor GetMotor(int motorId)
        {
            return _motorDal.GetMotor(motorId);
        }
        public void AddMotor(string merk, int bouwjaar, int prijs) 
        {
            _motorDal.AddMotor(merk, bouwjaar, prijs);
        }
        public void DeleteMotor(int motorId)
        {
            _motorDal.DeleteMotor(motorId);
        }
    }
}
