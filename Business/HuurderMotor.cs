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
        public bool? IsGeaccepteerd { get; set; }
        public int Prijs { get; set; }

        private readonly IHuurderMotorDal _huurderMotorDal;
        private readonly IHuurderMotorCollectionDal _huurderMotorCollectionDal;
        public HuurderMotor(IHuurderMotorDal i, IHuurderMotorCollectionDal h)
        {
            _huurderMotorDal = i;
            _huurderMotorCollectionDal = h;
        }

        public void AcceptOrDeclineRent(int huurderMotorId, bool AcceptRent)
        {
            _huurderMotorDal.AcceptOrDeclineRent(huurderMotorId, AcceptRent);
        }
        public bool CheckAvailability(int motorId, DateTime ophaal, DateTime inlever)
        {
            List<HuurderMotor> controlList = _huurderMotorCollectionDal.GetHuurderMotorListForMotor(motorId);

            if (controlList.Count > 0)
            {
                foreach (var item in controlList)
                {
                    if(ophaal <= item.InleverDatum && inlever >= item.OphaalDatum && item.IsGeaccepteerd == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
