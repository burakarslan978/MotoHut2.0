using Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HuurderMotorCollection : IHuurderMotorCollection
    {
        private readonly IHuurderMotorCollectionDal _huurderMotorCollectionDal;
        private readonly IHuurderMotorDal _huurderMotorDal;

        public HuurderMotorCollection(IHuurderMotorCollectionDal i, IHuurderMotorDal h)
        {
            _huurderMotorCollectionDal = i;
            _huurderMotorDal = h;
        }

        public List<HuurderMotor> GetHuurderMotorList()
        {
            return _huurderMotorCollectionDal.GetHuurderMotorList();
        }

        public List<HuurderMotor> GetHuurderMotorListForMotor(int motorId)
        {
            return _huurderMotorCollectionDal.GetHuurderMotorListForMotor(motorId);
        }

        public void DeleteHuurderMotorForMotor(int motorId)
        {
            _huurderMotorCollectionDal.DeleteHuurderMotorForMotor(motorId);
        }

        public List<HuurderMotor> DeclineOverlappingRents(int huurderMotorId, int motorId, DateTime ophaalDatum, DateTime inleverDatum)
        {
            List<HuurderMotor> list = _huurderMotorCollectionDal.GetHuurderMotorListForMotor(motorId);
            HuurderMotor huurderMotor = new HuurderMotor(_huurderMotorDal, _huurderMotorCollectionDal);
            foreach (var item in list)
            {
                if (CheckIfOverlaps(item, huurderMotorId, ophaalDatum, inleverDatum))
                {
                    huurderMotor.AcceptOrDeclineRent(item.HuurderMotorId, false);
                    item.IsGeaccepteerd = false;
                }
            }
            return list;
        }

        private bool CheckIfOverlaps(HuurderMotor huurderMotor, int huurderMotorId, DateTime ophaalDatum, DateTime inleverDatum)
        {
            return (huurderMotor.HuurderMotorId != huurderMotorId
                    && huurderMotor.IsGeaccepteerd != true
                    && huurderMotor.OphaalDatum <= inleverDatum
                    && huurderMotor.InleverDatum >= ophaalDatum);
        }



    }
}
