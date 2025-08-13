using clsDataAccessLayer;
using ConnectionDataBaseLincense;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsLicenseDataAccess
    {
        /*
        Licenses

        LicenseID
        ApplicationID
        DriverID
        LicenseClassID
        IssueDate
        ExpirationDate
        Notes
        PaidFees
        IsActive
        IssueReason
        CreatedByUserID    


        */

        //here
        static public int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID, DateTime ExpirationDate,
                                        string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "INSERT INTO [Licenses] " +
                                   "([ApplicationID], " +
                                   "[DriverID], " +
                                   "[LicenseClassID], " +
                                   "[ExpirationDate], " +
                                   "[Notes], " +
                                   "[PaidFees], " +
                                   "[IsActive], " +
                                   "[CreatedByUserID]," +
                                   "[IssueReason]) " +
                                   "VALUES " +
                                   "(@ApplicationID, " +
                                   "@DriverID, " +
                                   "@LicenseClassID, " +
                                   "@ExpirationDate, " +
                                   "@Notes, " +
                                   "@PaidFees, " +
                                   "@IsActive, " +
                                   "@CreatedByUserID," +
                                   "@IssueReason) " +
                                   "SELECT SCOPE_IDENTITY();";

                    // This function will return the new LicenseID if successful, or -1 if not.

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                        command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

                        if (string.IsNullOrEmpty(Notes))
                            command.Parameters.AddWithValue("@Notes", DBNull.Value);
                        else command.Parameters.AddWithValue("@Notes", Notes);

                        command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@IssueReason", IssueReason);
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


        static public bool FindLicenseByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate,
                                           ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive,
                                             ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // License was found
                            isFound = true;
                            ApplicationID = (int)reader["ApplicationID"];
                            DriverID = (int)reader["DriverID"];
                            LicenseClass = (int)reader["LicenseClassID"];
                            IssueDate = (DateTime)reader["IssueDate"];
                            ExpirationDate = (DateTime)reader["ExpirationDate"];
                            Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : null;
                            PaidFees = (decimal)reader["PaidFees"];
                            IsActive = (bool)reader["IsActive"];
                            IssueReason = (byte)reader["IssueReason"];
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

        public static bool UpdateLicenseActivationStatus(int licenseID, bool newActivationStatus)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "UPDATE Licenses SET IsActive = @NewActivationStatus WHERE LicenseID = @LicenseID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", licenseID);
                        command.Parameters.AddWithValue("@NewActivationStatus", newActivationStatus);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }
            return false;
               
            
        }


        //here
        static public int GetLicenseIDByApplicationID(int ApplicationID)
        {
            int LicenseID = -1;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT LicenseID FROM Licenses WHERE ApplicationID = @ApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // License was found
                            LicenseID = (int)reader["LicenseID"];
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }


            return LicenseID;
        }

        public static bool IsLicenseExpired(int licenseID)
        {


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT 1 FROM Licenses WHERE Licenses.ExpirationDate < CAST(GETDATE() AS DATE) AND Licenses.LicenseID = @LicenseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", licenseID);


                        connection.Open();

                        object result = command.ExecuteScalar();

                        return result != null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

                return false;

        }
    
     


        static public bool IsLicenseExistByLicenseID(int LicenseID)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "SELECT 1 FROM Licenses WHERE LicenseID = @LicenseID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);


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


        public static DataTable GetAllPersonLicenses(int PersonID)
        {
            DataTable licensesTable = new DataTable();


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"
                 SELECT Licenses.LicenseID AS 'License ID',
                        Licenses.ApplicationID AS 'LDLApp ID',
                 	    Licenses.DriverID AS 'Driver ID',
                 	    (SELECT LicenseClasses.ClassName Where LicenseClasses.LicenseClassID = Licenses.LicenseClassID) AS 'Class Name',
                 	    Licenses.IssueDate AS 'Issue Date',
                 	    Licenses.ExpirationDate AS 'Expiared Date',
                       CASE 
	                   When  Licenses.IsActive = 1 THEN 'YES'
	                   Else 'NO' End As 'Is Acrive'
                 From Licenses INNER JOIN LicenseClasses ON LicenseClasses.LicenseClassID = Licenses.LicenseClassID
                               INNER JOIN Drivers ON Drivers.DriverID = Licenses.DriverID 
                 			   INNER JOIN People ON People.PersonID = Drivers.PersonID
                 WHERE People.PersonID =  @PersonID";

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



        public static int GetPerosnIDByLicenseID(int LicenseID)
        {
            int PersonID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT People.PersonID FROM People " +
                                   "INNER JOIN Drivers ON Drivers.PersonID = People.PersonID " +
                                   "INNER JOIN Licenses ON Licenses.DriverID = Drivers.DriverID " +
                                   "WHERE Licenses.LicenseID = @LicenseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // PersonID was found
                            PersonID = (int)reader["PersonID"];
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
              clsGlobalDataAccess.LogError(ex);
            }

            return PersonID;
        }


        public static int GetLicenseIDByPersonIDAndLicenceClassID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClassID = @LicenseClass 
                              AND Drivers.PersonID = @PersonID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);


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




        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            int LicenseID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = @"SELECT Licenses.LicenseID
                            FROM Licenses INNER JOIN
                            Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             Licenses.LicenseClassID = @LicenseClassID
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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



    }
}
