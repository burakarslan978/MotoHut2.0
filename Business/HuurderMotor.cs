using Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HuurderMotor : IHuurderMotor
    {
        public HuurderMotor() { }
        public int HuurderMotorId { get; set; }
        public int MotorId { get; set; }
        public int HuurderId { get; set; }
        public DateTime OphaalDatum { get; set; }
        public DateTime InleverDatum { get; set; }
        public bool IsGeaccepteerd { get; set; }
        public bool IsGeweigerd { get; set; }

        private readonly IHuurderMotorDal _huurderMotorDal;
        public HuurderMotor(IHuurderMotorDal i)
        {
            _huurderMotorDal = i;
        }

        public void AcceptOrDeclineRent(int huurderMotorId, string acceptOrDecline)
        {
            _huurderMotorDal.AcceptOrDeclineRent(huurderMotorId, acceptOrDecline);
        }
        public bool CheckAvailability(int motorId, DateTime ophaal, DateTime inlever)
        {
            return _huurderMotorDal.CheckAvailability(motorId, ophaal, inlever);
        }
    }
}
