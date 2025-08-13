

using System;
using DataAccessLayer;
using System.Data;


namespace BusinessLayer
{
    public class clsCountry
    {
        public string CountryName { get; }
        public int CountryID { get; }

        public clsCountry()
        {
            CountryName = "";
            CountryID = -1;
        }

        private clsCountry(string countryName, int countryID)
        {
            CountryName = countryName;
            CountryID = countryID;
        }

        static public clsCountry Find(int CountryID)
        {
            string CountryName = "";

            if (clsCountriesDataAccess.FindCountry(CountryID, ref CountryName))
                return new clsCountry(CountryName, CountryID);
            else return null;



        }
        static public clsCountry FindCountry(string countryName)
        {
            int CountryID = -1;
            if (clsCountriesDataAccess.FindCountry(countryName, ref CountryID))
                return new clsCountry(countryName, CountryID);
            else return null;

        }

        static public DataTable GetCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();
        }

    }
}
