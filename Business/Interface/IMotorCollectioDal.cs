using MotoHut2._0.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IMotorCollectionDal
    {
        public List<Motor> GetMotorList();
        public void AddMotor(string merk, int bouwjaar, int prijs, bool huurbaar, int verhuurderId);
        public void DeleteMotor(int motorId);
    }
}
