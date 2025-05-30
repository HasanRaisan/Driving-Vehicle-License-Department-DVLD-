using ConnectionDataBaseLincense;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDataAccessLayer
{
    public class clsInternationalLicenseDataAccess
    {


        static public int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
                                             DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int LicenseID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "INSERT INTO [InternationalLicenses] " +
                                   "([ApplicationID], " +
                                   "[DriverID], " +
                                   "[IssuedUsingLocalLicenseID], " +
                                   "[ExpirationDate], " +
                                   "[IsActive], " +
                                   "[CreatedByUserID]) " +
                                   "VALUES " +
                                   "(@ApplicationID, " +
                                   "@DriverID, " +
                                   "@IssuedUsingLocalLicenseID, " +
                                   "@ExpirationDate, " +
                                   "@IsActive, " +
                                   "@CreatedByUserID) " +
                                   "SELECT SCOPE_IDENTITY();";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                        command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LicenseID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return LicenseID;
        }



        static public bool FindInternationalLicenseByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,
                                                ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM InternationalLicenses WHERE LicenseID = @LicenseID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            ApplicationID = (int)reader["ApplicationID"];
                            DriverID = (int)reader["DriverID"];
                            IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                            IssueDate = (DateTime)reader["IssueDate"];
                            ExpirationDate = (DateTime)reader["ExpirationDate"];
                            IsActive = (bool)reader["IsActive"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
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


        static public bool IsInternationalLicenseExistByLicenseID(int InternationalLicenseID)
        {
            bool isExist = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT 1 FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        static public bool IsLocalLicenseUsedByLicenseID(int LocalLicenseID, ref int InternationalLicensenID)
        {

            bool isExist = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT InternationalLicenseID FROM InternationalLicenses
                             WHERE InternationalLicenses.IssuedUsingLocalLicenseID = @LocalLicenseID ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isExist = reader.HasRows;
                            InternationalLicensenID = (int)reader["InternationalLicenseID"];
                        }

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

        // form Show License
        public static bool FindInternationalLicenseDetailsByID(
                                     int InternationalLicenseID,
                                     ref string FullName,
                                     ref string Gender,
                                     ref string IsActive,
                                     ref int LicenseID,
                                     ref string NationalNo,
                                     ref DateTime IssueDate,
                                     ref DateTime ExpirationDate,
                                     ref DateTime DateOfBirth,
                                     ref int DriverID, ref int ApplicationID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"
        SELECT 
            (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) AS FullName,
            CASE 
                WHEN People.Gendor = 1 THEN 'Male' 
                WHEN People.Gendor = 0 THEN 'Female' 
            END AS Gendor,
            CASE 
                WHEN InternationalLicenses.IsActive = 1 THEN 'YES' 
                WHEN InternationalLicenses.IsActive = 0 THEN 'NO' 
            END AS IsActive,
            InternationalLicenses.InternationalLicenseID,
            Licenses.LicenseID,
            People.NationalNo,
            InternationalLicenses.IssueDate,
            InternationalLicenses.ApplicationID,
            InternationalLicenses.ExpirationDate,
            People.DateOfBirth,
            Drivers.DriverID
        FROM InternationalLicenses
        INNER JOIN Drivers 
            ON InternationalLicenses.DriverID = Drivers.DriverID 
        INNER JOIN Licenses 
            ON InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID 
            AND Drivers.DriverID = Licenses.DriverID 
        INNER JOIN People 
            ON Drivers.PersonID = People.PersonID
        WHERE InternationalLicenses.InternationalLicenseID = @InternationalLicenseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;

                            FullName = reader["FullName"].ToString();
                            Gender = reader["Gendor"] != DBNull.Value ? reader["Gendor"].ToString() : "Unknown";
                            IsActive = reader["IsActive"] != DBNull.Value ? reader["IsActive"].ToString() : "Unknown";
                            LicenseID = reader["LicenseID"] != DBNull.Value ? (int)reader["LicenseID"] : -1;
                            NationalNo = reader["NationalNo"]?.ToString();
                            IssueDate = reader["IssueDate"] != DBNull.Value ? (DateTime)reader["IssueDate"] : DateTime.MinValue;
                            ExpirationDate = reader["ExpirationDate"] != DBNull.Value ? (DateTime)reader["ExpirationDate"] : DateTime.MinValue;
                            DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? (DateTime)reader["DateOfBirth"] : DateTime.MinValue;
                            DriverID = reader["DriverID"] != DBNull.Value ? (int)reader["DriverID"] : -1;
                            ApplicationID = reader["ApplicationID"] != DBNull.Value ? (int)reader["ApplicationID"] : -1;
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

        static public DataTable GetInternationalApplications()
        {
            DataTable dtPeople = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"               SELECT 
            InternationalLicenses.InternationalLicenseID AS 'Int.License ID',
            InternationalLicenses.ApplicationID AS 'Application ID',
			Drivers.DriverID AS 'Driver ID',
            InternationalLicenses.IssuedUsingLocalLicenseID AS 'L.License ID',

            InternationalLicenses.IssueDate AS 'Issue Date',
            InternationalLicenses.ExpirationDate AS 'Expiration Date',

            CASE 
                WHEN InternationalLicenses.IsActive = 1 THEN 'YES' 
                WHEN InternationalLicenses.IsActive = 0 THEN 'NO' 
            END AS 'Is Active'
        FROM InternationalLicenses
        INNER JOIN Drivers 
            ON InternationalLicenses.DriverID = Drivers.DriverID ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                            dtPeople.Load(reader);
                    }
                }
            }
            catch (Exception ex) { clsGlobalDataAccess.LogError(ex); }
            return dtPeople;
        }


        public static DataTable GetAllLicenseInternationalForPersonByPersonID(int PersonID)
        {

            DataTable licensesTable = new DataTable();

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"        SELECT 
            InternationalLicenses.InternationalLicenseID AS 'Int.License ID',
            InternationalLicenses.ApplicationID AS 'Application ID',
			Drivers.DriverID AS 'Driver ID',
            InternationalLicenses.IssuedUsingLocalLicenseID AS 'L.License ID',
           

            InternationalLicenses.IssueDate AS 'Issue Date',
            InternationalLicenses.ExpirationDate AS 'Expiration Date',
             CASE 
                WHEN InternationalLicenses.IsActive = 1 THEN 'YES' 
                WHEN InternationalLicenses.IsActive = 0 THEN 'NO' 
            END AS 'Is Active'

        FROM InternationalLicenses
        INNER JOIN Drivers 
            ON InternationalLicenses.DriverID = Drivers.DriverID 
		INNER JOIN People 
		     ON  People.PersonID = Drivers.PersonID
			 where People.PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(licensesTable);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }
            return licensesTable;
        }

    }
   
}
