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
        private readonly IHuurderMotorDal _huurderMotorDal;
        public HuurderMotorCollection(IHuurderMotorDal i)
        {
            _huurderMotorDal = i;
        }

        public List<HuurderMotor> GetHuurderMotorList()
        {
            return _huurderMotorDal.GetHuurderMotorList();
        }
    }
}
