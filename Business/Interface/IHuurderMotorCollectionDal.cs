﻿using MotoHut2._0.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IHuurderMotorCollectionDal
    {
        public List<HuurderMotor> GetHuurderMotorList();
        public List<HuurderMotor> GetHuurderMotorListForMotor(int motorId);
        public void DeleteHuurderMotorForMotor(int motorId);
    }
}
