using clsDataAccessLayer;
using ConnectionDataBaseLincense;
using System;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessLayer
{
    public class clsDetainLicenseDataAccess
    {


        static public bool IsLicenseDetainedByLicenseID(int LicenseID)
        {
            bool isDetained = false;


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = " Select 1 from DetainedLicenses" +
                        " Where DetainedLicenses.LicenseID = @LicenseID AND DetainedLicenses.IsReleased = 0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        isDetained = (result != null);

                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }


            return isDetained;
        }

        public static int DetaineLicense(int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID)
        {

            int DetainID = -1;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"
                INSERT INTO DetainedLicenses
                ([LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID])
                VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, 0, NULL, NULL, NULL)  SELECT SCOPE_IDENTITY()";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", licenseID);
                        command.Parameters.AddWithValue("@DetainDate", detainDate);
                        command.Parameters.AddWithValue("@FineFees", fineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                        connection.Open();

                        object insertedID = command.ExecuteScalar();
                        if (insertedID != null && int.TryParse(insertedID.ToString(), out int ID))
                        {
                            DetainID = ID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return DetainID;
                
            
        }

        public static bool ReleaseDetainedLicense(int licenseID, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"
            UPDATE DetainedLicenses
            SET [IsReleased] = 1,
                [ReleaseDate] = @ReleaseDate,
                [ReleasedByUserID] = @ReleasedByUserID,
                [ReleaseApplicationID] = @ReleaseApplicationID
            WHERE [LicenseID] = @LicenseID AND [IsReleased] = 0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", licenseID);
                        command.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                        command.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserID);
                        command.Parameters.AddWithValue("@ReleaseApplicationID", releaseApplicationID);


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
           
        

        public static bool FindDetainedLicenseByLicenseID(int licenseID, ref int detainID, ref DateTime detainDate, ref decimal fineFees,
                                                  ref int createdByUserID, ref bool isReleased, ref DateTime? releaseDate, ref int? releasedByUserID,
                                                  ref int? releaseApplicationID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", licenseID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Update ref parameters with data from the database
                            detainID = (int)reader["DetainID"];
                            detainDate = (DateTime)reader["DetainDate"];
                            fineFees = (decimal)reader["FineFees"];
                            createdByUserID = (int)reader["CreatedByUserID"];
                            isReleased = (bool)reader["IsReleased"];
                            releaseDate = reader["ReleaseDate"] as DateTime?;
                            releasedByUserID = reader["ReleasedByUserID"] as int?;
                            releaseApplicationID = reader["ReleaseApplicationID"] as int?;


                    //        releaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value? (int?)reader["ReleaseApplicationID"]: null;



                            isFound = true;
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



        public static DataTable GetAllDetains()
        {

            DataTable detainedDataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"
                   SELECT 
                       DetainedLicenses.DetainID AS 'D.ID',
                       DetainedLicenses.LicenseID AS 'L.ID',
                       DetainedLicenses.DetainDate AS 'D.Date',
                       CASE
                           WHEN DetainedLicenses.IsReleased = 0 THEN 'No'
                           WHEN DetainedLicenses.IsReleased = 1 THEN 'Yes'
                       END AS 'Is Released',
                       DetainedLicenses.FineFees AS 'Fine Fees',
                       DetainedLicenses.ReleaseDate AS 'Release Date',
                       People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + ISNULL(People.LastName, '') AS 'Full Name',
                       People.NationalNo AS 'N.NO',
                       DetainedLicenses.ReleaseApplicationID AS 'Release App ID'
                   FROM 
                       DetainedLicenses 
                   INNER JOIN 
                       Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
                   INNER JOIN 
                       Drivers ON Licenses.DriverID = Drivers.DriverID
                   INNER JOIN 
                       People ON People.PersonID = Drivers.PersonID";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            detainedDataTable.Load(reader);
                        }
                    }
                }

            }
            catch(Exception ex) 
            {
                clsGlobalDataAccess.LogError(ex);
            }
                
                return detainedDataTable;
            
        }

        public static bool IsThisDetainReleaseByDetainID(int DetainID)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT 1 FROM DetainedLicenses WHERE DetainID = @DetainID AND IsReleased = 0";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@DetainID", DetainID);

                        object result = sqlCommand.ExecuteScalar();
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
    }
}
