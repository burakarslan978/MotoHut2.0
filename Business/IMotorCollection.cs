using Business;

using System.Data.SqlClient;

namespace MotoHut2._0.Collections
{
    public interface IMotorCollection
    {
        public List<Motor> ConvertDataToView();

        public void ReadyReader(SqlDataReader rdr)
        {
            Motor model = new Motor();
            model.MotorId = (int)rdr["MotorId"];
            model.VerhuurderId = (int)rdr["VerhuurderId"];
            model.Model = (string)rdr["Model"];
            model.Bouwjaar = (int)rdr["Bouwjaar"];
            model.Prijs = (int)rdr["Prijs"];
            model.Status = (string)rdr["Status"];
        }
    }
}
