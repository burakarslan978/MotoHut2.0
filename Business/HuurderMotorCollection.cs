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
        public HuurderMotorCollection(IHuurderMotorCollectionDal i)
        {
            _huurderMotorCollectionDal = i;
        }

        public List<HuurderMotor> GetHuurderMotorList()
        {
            return _huurderMotorCollectionDal.GetHuurderMotorList();
        }

        public List<HuurderMotor> GetHuurderMotorListForMotor(int motorId)
        {
            return _huurderMotorCollectionDal.GetHuurderMotorListForMotor(motorId);
        }
    }
}
