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
        public void AcceptOrDeclineRent(int huurderMotorId, bool AcceptRent);
        public bool CheckAvailability(int motorId, DateTime ophaal, DateTime inlever);
    }
}
