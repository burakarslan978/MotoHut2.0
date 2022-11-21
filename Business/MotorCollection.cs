using Business;
using MotoHut2._0.Collections;

namespace MotoHut2._0
{
    public class MotorCollection : IMotorCollection
    {
        private readonly IMotorDal _motorDal;
        public MotorCollection(IMotorDal i)
        {
            _motorDal = i;
        }

        public List<Motor> GetMotorList()
        {
            //List<Motor> list = new List<Motor>();
            //foreach(var item in _motorDal.MotorControl())
            //{
            //    Motor motor = new Motor();
            //    motor.MotorId = item.MotorId;
            //    motor.VerhuurderId = item.VerhuurderId;
            //    motor.Bouwjaar = item.Bouwjaar;
            //    motor.Prijs = item.Prijs;
            //    motor.Model = item.Model;
            //    motor.Status = item.Status;

            //    list.Add(item);
            //}
            return _motorDal.GetMotorList();
        }
    }
}
