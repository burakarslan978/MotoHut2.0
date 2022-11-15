using MotoHut2._0.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IMotorDal
    {
        public List<Motor> MotorControl();
        public void RentMotorDal(int motorId);
        public Motor GetMotor(int motorId);
        public void AddMotor(string merk, int bouwjaar, int prijs);
    }
}
