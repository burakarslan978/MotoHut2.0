using Business;
using MotoHut2._0.Collections;

namespace MotoHut2._0
{
    public class MotorCollection : IMotorCollection
    {
        private readonly IMotorCollectionDal _motorCollectionDal;

        public MotorCollection(IMotorCollectionDal i)
        {
            _motorCollectionDal = i;
        }
        public List<Motor> GetMotorList()
        {
            return _motorCollectionDal.GetMotorList();
        }

        public void AddMotor(string merk, int bouwjaar, int prijs, bool huurbaar)
        {
            _motorCollectionDal.AddMotor(merk, bouwjaar, prijs, huurbaar);
        }
        public void DeleteMotor(int motorId)
        {
            _motorCollectionDal.DeleteMotor(motorId);
        }
    }
}
