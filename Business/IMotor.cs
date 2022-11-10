using System.Data.SqlClient;

namespace Business
{
    public interface IMotor
    {
        void ReadyReader(SqlDataReader rdr);
    }
}