using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;
using clsDataAccessLayer;
using ConnectionDataBaseLincense;

namespace DataAccessLayer
{
    public class clsPersonDataAccess
    {
        static public bool FindPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName,
             ref string LastName, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone, ref string Email,
             ref int NationalityCountryID, ref string ImagePath)
        {


            bool isFound=false;

            try 
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM People WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            //Person was found
                            isFound = true;
                            NationalNo = (string)reader["NationalNo"];
                            FirstName = (string)reader["FirstName"];
                            SecondName = (string)reader["SecondName"];
                            ThirdName = (string)reader["ThirdName"];
                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gendor = (byte)reader["Gendor"];
                            Address = (string)reader["Address"];
                            Phone = (string)reader["Phone"];
                            NationalityCountryID = (int)reader["NationalityCountryID"];
                            Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                            ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";


                        }
                        reader.Close();

                    }
                }

            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }
            return isFound;        
        }



        static public bool FindPersonByNationalNo(string NationalNo,  ref int PersonID , ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool idFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            //Person was found
                            idFound = true;
                            PersonID = (int)reader["PersonID"];
                            FirstName = (string)reader["FirstName"];
                            SecondName = (string)reader["SecondName"];
                            ThirdName = (string)reader["ThirdName"];
                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Address = (string)reader["Address"];
                            Phone = (string)reader["Phone"];
                            Email = (string)reader["Email"];
                            NationalityCountryID = (int)reader["NationalityCountryID"];
                            ImagePath = (string)reader["ImagePath"];
                            Gendor = (byte)reader["Gendor"];

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return idFound;
        }


        static public bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName,
              string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email,
              int NationalityCountryID, string ImagePath)
        { 
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "\r\nUPDATE [People]\r\n   SET   [FirstName] = @FirstName\r\n    ,[SecondName] = @SecondName\r\n   " +
                        "   ,[ThirdName] = @ThirdName\r\n      ,[LastName] = @LastName\r\n     " +
                        " ,[DateOfBirth] = @DateOfBirth\r\n      ,[Gendor] = @Gendor\r\n     " +
                        " ,[Address] = @Address\r\n      ,[Phone] = @Phone\r\n      ,[Email] = @Email\r\n " +
                        "     ,[NationalityCountryID] = @NationalityCountryID\r\n   " +
                        "   ,[ImagePath] = @ImagePath, [NationalNo] = @NationalNo\r\n WHERE PersonID = @PersonID\r\n";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@SecondName", SecondName);
                        command.Parameters.AddWithValue("@ThirdName", ThirdName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Gendor", Gendor);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();

                    }
                }
            }
            
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return (rowAffected > 0);
        }


        static public int AddPerson( string NationalNo, string FirstName, string SecondName, string ThirdName,
              string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email,
              int NationalityCountryID, string ImagePath)
        {

            int PersonID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "INSERT INTO  [People]" +
                        "([NationalNo]" +
                        ",[FirstName]" +
                        ",[SecondName]" +
                        ",[ThirdName]" +
                        ",[LastName]" +
                        ",[DateOfBirth]" +
                        ",[Gendor]" +
                        ",[Address]" +
                        ",[Phone]" +
                        ",[Email]" +
                        ",[NationalityCountryID]" +
                        ",[ImagePath])                   Values" +
                        "(@NationalNo, " +
                        "@FirstName," +
                        "@SecondName," +
                        "@ThirdName," +
                        "@LastName," +
                        "@DateOfBirth," +
                        "@Gendor," +
                        "@Address," +
                        "@Phone," +
                        "@Email," +
                        "@NationalityCountryID," +
                        "@ImagePath) SELECT SCOPE_IDENTITY();";

                    //this function will return the new perosn id if succeeded and -1 if not.

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@SecondName", SecondName);
                        command.Parameters.AddWithValue("@ThirdName", ThirdName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Gendor", Gendor);
                        command.Parameters.AddWithValue("@Address", Address);

                        if (!string.IsNullOrEmpty(Email))
                            command.Parameters.AddWithValue("@Email", Email);
                        else command.Parameters.AddWithValue("@Email", DBNull.Value);

                        command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

                        if (!string.IsNullOrEmpty(ImagePath))
                            command.Parameters.AddWithValue("@ImagePath", ImagePath);
                        else
                            command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

                        command.Parameters.AddWithValue("@Phone", Phone);


                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            PersonID = insertedID;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }
            return PersonID;
        }


        static public bool DeletePerson(int PersonID)
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "DELETE from People WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return (rowAffected > 0);
        }



        static public bool IsPersonExistByPersonID(int PersonID)
        {
            bool isExist = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT 1 FROM People WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(@query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        isExist = reader.HasRows;
                        reader.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isExist;
        }


        static public bool IsPersonExist(string NationalNo)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT 1 FROM People WHERE NationalNo = @NationalNo";
                    using (SqlCommand command = new SqlCommand(@query, connection))
                    {
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        isExist = reader.HasRows;
                        reader.Close();

                    }
                }
            }

            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isExist;
        }

        static public DataTable GetPeople()
        {
            DataTable dtPeople = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT PersonID AS 'Person ID', NationalNo AS 'National No', FirstName AS 'First Name',
                            SecondName AS 'Second Name', ThirdName AS 'Third Name', LastName AS 'Last Name',
                              Case 
                              WHEN Gendor = 1 THEN 'Male'
                              WHEN Gendor = 0 THEN 'Female'
                      		End
                      AS 'Gender', Address, Phone, Email,
                      Countries.CountryName
                      FROM   People INNER JOIN Countries ON Countries.CountryID =  People.NationalityCountryID ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dtPeople.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtPeople;
        }


        public static bool IsNationalNoExists(string NationalNo)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT COUNT(1) FROM People WHERE NationalNo = @NationalNo";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);


                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        isFound = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }
            return isFound;

        }
            
        static public string GetNationalNoByPersonID(int PersonID)
        {
            string nationalNo = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT NationalNo FROM People WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            nationalNo = (string)reader["NationalNo"];
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return nationalNo;
        }

        static public int GetPersonIDByNationalNo(string NationalNo)
        {
            int personID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT PersonID FROM People WHERE NationalNo = @NationalNo";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);


                        connection.Open();
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            personID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return personID; 
        }



    }

    public class clsCountriesDataAccess
    {

        static public bool FindCountry ( int countryID,ref string countryName)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryID", countryID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;

                            countryName = (string)reader["CountryName"];
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isFound;
        }
        static public bool FindCountry (string countryName, ref int countryID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryName", countryName);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            isFound = true;
                            countryID = (int)reader["CountryID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }
            return isFound;
        }
        static public DataTable GetAllCountries()
        {
            DataTable dtCountries = new DataTable();

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Countries";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) dtCountries.Load(reader);
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtCountries;
        }


    }
}
