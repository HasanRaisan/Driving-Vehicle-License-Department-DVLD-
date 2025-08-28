using System.Configuration;

namespace ConnectionDataBaseLincense
{
    public class clsDataAccessSettings
    {
        public static string ConnectionString =
            ConfigurationManager.ConnectionStrings["DVLDconnection"].ConnectionString;
    }
}
