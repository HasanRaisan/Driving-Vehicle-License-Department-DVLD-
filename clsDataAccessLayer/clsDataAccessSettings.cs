using System;
using System.Configuration;


namespace ConnectionDataBaseLincense
{
    public class clsDataAccessSettings
    {
        public static string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=sa123456;";
       // public static string ConnectionString = ConfigurationManager.AppSettings["DVLDconnection"];
    }
}
