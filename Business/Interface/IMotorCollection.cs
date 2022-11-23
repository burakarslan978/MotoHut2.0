using Business;

using System.Data.SqlClient;

namespace MotoHut2._0.Collections
{
    public interface IMotorCollection
    {
        public List<Motor> GetMotorList();
        public void AddMotor(string merk, int bouwjaar, int prijs, bool huurbaar);
        public void DeleteMotor(int motorId);

    }
}
