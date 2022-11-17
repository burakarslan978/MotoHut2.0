using MotoHut2._0.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IHuurderMotorDal
    {
        public List<HuurderMotor> GetHuurderMotorList();
    }
}
