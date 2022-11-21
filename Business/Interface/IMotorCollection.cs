using Business;

using System.Data.SqlClient;

namespace MotoHut2._0.Collections
{
    public interface IMotorCollection
    {
        public List<Motor> ConvertDataToView();


    }
}
