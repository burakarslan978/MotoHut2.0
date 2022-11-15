using System.Data.SqlClient;

namespace Business
{
    public interface IMotor
    {
        void ReadyReader(SqlDataReader rdr);

        public void RentMotor(int motorId);
        public Motor GetMotor(int motorId);
        public void AddMotor(string merk, int bouwjaar, int prijs);
    }
}