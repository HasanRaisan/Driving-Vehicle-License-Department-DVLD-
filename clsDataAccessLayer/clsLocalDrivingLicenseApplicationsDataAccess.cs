using clsDataAccessLayer;
using ConnectionDataBaseLincense;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsLocalDrivingLicenseApplicationsDataAccess
    {
        static public int AddNewLocalDrivingLicenseApplications(int ApplicationID, int LicenseClassID)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "INSERT INTO LocalDrivingLicenseApplications (ApplicationID,  LicenseClassID)   VALUES" +
                        "(@ApplicationID,  @LicenseClassID) SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        int LocalDrivingLicenseApplicationID = -1;


                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int IDResult))
                        {
                            LocalDrivingLicenseApplicationID = IDResult;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return ApplicationID;

        }


        static public DataTable GeLAllLocalDrivingLicenseApplications()
        {
            DataTable dtLDLApps = new DataTable();

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT * FROM LocalDrivingLicenseApplicationsDataAccess 
                                 order by ApplicationDate Desc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) { dtLDLApps.Load(reader); }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }
            return dtLDLApps;
        }

        static public DataTable GeLAllLocalDrivingLicenseApplicationsForMange()
        {
            DataTable dtLDLApps = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = @"SELECT 
                   LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS 'L.D.L.AppsID', 
                   LicenseClasses.ClassName AS 'Class Name', 
                   People.NationalNo AS 'National No.', 
                   People.FirstName + ' ' + People.SecondName + ' ' + People.LastName AS 'Full Name',
                   Applications.ApplicationDate AS 'Application Date', 

                     (  select count( cast (Tests.TestResult as int) )
				  from Tests
				   where Tests.TestAppointmentID in
				   ( select TestAppointments.TestAppointmentID 
				        from TestAppointments
						  Where TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID and TestResult = 1 )
				       )  As 'Passed Tests',

                   CASE 
                       WHEN Applications.ApplicationStatus = 1 THEN 'New'
                       WHEN Applications.ApplicationStatus = 2 THEN 'Canceled'
                       WHEN Applications.ApplicationStatus = 3 THEN 'Completed'
                       ELSE 'Unknown'
                   END AS 'Application Status'
               FROM 
                   LocalDrivingLicenseApplications
               INNER JOIN 
                   LicenseClasses 
                   ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
               INNER JOIN 
                   Applications 
                   ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
               INNER JOIN 
                   People 
                   ON Applications.ApplicantPersonID = People.PersonID; ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) { dtLDLApps.Load(reader); }
                        reader.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtLDLApps;
        }

        static public bool GetLocalDrivingLicenseApplicationInfoByApplicationID(int ApplicationID, ref int LDLAppID, ref int LicenseClassID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            //app was found

                            isFound = true;
                            LDLAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                            LicenseClassID = (byte)reader["LicenseClassID"];
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

        static public bool UpdateLicenseClass(int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "Update LocalDrivingLicenseApplications SET LicenseClassID = @LicenseClassID WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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

        static public bool GetLocalDrivingLicenseApplicationInfoByID(int LDLAppID, ref int ApplicationID, ref int LicenseClassID)
        {


            bool isFound = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LDLAppID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LDLAppID", LDLAppID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            //app was found

                            isFound = true;
                            ApplicationID = (int)reader["ApplicationID"];
                            LicenseClassID = (int)reader["LicenseClassID"];
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



        static public bool DeleteLocalDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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

        static public bool DoseLocalDrvingLicenseApplicationPassThisTest(int LocalDrivingLicenseAppID, int TestTypeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @" SELECT Found = 1
                  FROM Tests
                  INNER JOIN TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                  INNER JOIN LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                  WHERE TestAppointments.TestTypeID = @TestTypeID 
                    AND Tests.TestResult = 1 
                    AND LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);


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

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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


    }

}
