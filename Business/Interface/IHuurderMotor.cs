﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IHuurderMotor
    {
        public void AcceptOrDeclineRent(int huurderMotorId, bool AcceptRent);
        public bool CheckAvailability(int motorId, DateTime ophaal, DateTime inlever);
    }
}
