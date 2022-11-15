using System.Data.SqlClient;

namespace Business
{
    public interface IMotor
    {
        void ReadyReader(SqlDataReader rdr);

        public void RentMotor(int motorId);
    }
}