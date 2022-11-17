using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IHuurderMotorCollection
    {
        public List<HuurderMotor> GetHuurderMotorList();
        public List<HuurderMotor> GetHuurderMotorListForMotor(int motorId);
    }
}
