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

        static public int AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,
              string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email,
              int NationalityCountryID, string ImagePath)
        {
            int personID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("AddPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

                    command.Parameters.AddWithValue("@ThirdName", string.IsNullOrWhiteSpace(ThirdName) ? DBNull.Value : (object)ThirdName);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(Email) ? DBNull.Value : (object)Email);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrWhiteSpace(ImagePath) ? DBNull.Value : (object)ImagePath);

                    SqlParameter @NewPersonID = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    { Direction = ParameterDirection.Output };

                    command.Parameters.Add(@NewPersonID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    personID = (int)@NewPersonID.Value;
                }

            }
            catch
            (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return personID;
        }


        public static string GetNationalNoByPersonID(int PersonID)
        {
            string nationalNo = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("GetNationalNoByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    SqlParameter outputNationalNo = new SqlParameter("@NationalNo", SqlDbType.NVarChar, 20)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputNationalNo);

                    connection.Open();
                    command.ExecuteNonQuery();

                    if (outputNationalNo.Value != DBNull.Value)
                        nationalNo = (string)outputNationalNo.Value;
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return nationalNo;
        }


        public static int GetPersonIDByNationalNo(string NationalNo)
        {
            int personID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("GetPersonIDByNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    SqlParameter outputPersonID = new SqlParameter("@PersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputPersonID);

                    connection.Open();
                    command.ExecuteNonQuery();

                    personID = (int)outputPersonID.Value;
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return personID;
        }


    public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName,
                        string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email,
                        int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

                    command.Parameters.AddWithValue("@ThirdName", string.IsNullOrWhiteSpace(ThirdName) ? DBNull.Value : (object)ThirdName);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(Email) ? DBNull.Value : (object)Email);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrWhiteSpace(ImagePath) ? DBNull.Value : (object)ImagePath);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return rowsAffected > 0;
        }


        static public bool FindPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName,
                                                  ref string LastName, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone, ref string Email,
                                                    ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("FindPersonByNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        isFound = true;
                        PersonID = (int)reader["PersonID"];
                        FirstName = (string)reader["FirstName"];
                        SecondName = (string)reader["SecondName"];
                        ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                        LastName = (string)reader["LastName"];
                        DateOfBirth = (DateTime)reader["DateOfBirth"];
                        Gendor = (byte)reader["Gendor"];
                        Address = (string)reader["Address"];
                        Phone = (string)reader["Phone"];
                        Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                        NationalityCountryID = (int)reader["NationalityCountryID"];
                        ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isFound;
        }


        static public bool FindPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName,
                                          ref string LastName, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone, ref string Email,
                                          ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("FindPersonByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        isFound = true;
                        NationalNo = (string)reader["NationalNo"];
                        FirstName = (string)reader["FirstName"];
                        SecondName = (string)reader["SecondName"];
                        ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                        LastName = (string)reader["LastName"];
                        DateOfBirth = (DateTime)reader["DateOfBirth"];
                        Gendor = (byte)reader["Gendor"];
                        Address = (string)reader["Address"];
                        Phone = (string)reader["Phone"];
                        Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                        NationalityCountryID = (int)reader["NationalityCountryID"];
                        ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isFound;
        }



        static public bool DeletePerson(int PersonID)
        {
            bool isDeleted = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("DeletePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    isDeleted = (affectedRows > 0);
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isDeleted;
        }


        static public bool IsPersonExistNationalNo(string NationalNo)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("IsPersonExistByNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    SqlParameter outputParam = new SqlParameter("@Exists", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    isExist = Convert.ToBoolean(outputParam.Value);
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isExist;
        }




        static public bool IsPersonExistByPersonID(int PersonID)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("IsPersonExistByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    SqlParameter outputParam = new SqlParameter("@Exists", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    isExist = Convert.ToBoolean(outputParam.Value);
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isExist;
        }


        public static bool IsNationalNoExists(string NationalNo)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("IsNationalNoExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    SqlParameter outputParam = new SqlParameter("@Exists", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    isFound = Convert.ToBoolean(outputParam.Value);
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return isFound;
        }



        static public DataTable GetPeople()
        {
            DataTable dtPeople = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("GetPeople", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dtPeople.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtPeople;
        }


    }
    public class clsCountriesDataAccess
        {

            static public bool FindCountry(int countryID, ref string countryName)
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
            static public bool FindCountry(string countryName, ref int countryID)
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
