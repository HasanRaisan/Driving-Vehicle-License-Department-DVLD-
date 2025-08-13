using System;
using System.Data;
using System.Data.SqlClient;
using clsDataAccessLayer;
using ConnectionDataBaseLincense;

namespace DataAccessLayer
{
    public class clsApplicationsDataAccess
    {
       static  public int AddNewApplication(int ApplicantPersonID, int ApplicationTypeID, Decimal PaidFees, int CreatedByUsername, int LicenseClassID)
       {
            DateTime ApplicationDate = DateTime.Now;
            DateTime LastStatusDate = DateTime.Now;
            int ApplicationStatus = 1;

            int ApplicationID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus," +
                                   "LastStatusDate, PaidFees, CreatedByUserID, LicenseClassID)  " +
                                   "VALUES" +
                                   "(@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUsername, @LicenseClassID)" +
                                   " SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                        command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                        command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        command.Parameters.AddWithValue("@CreatedByUsername", CreatedByUsername);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int IDResult))
                        {
                            ApplicationID = IDResult;
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

        static public bool UpdateStatus(int ApplicationID , byte ApplicationStatus)
        {
            DateTime LastStatusDate = DateTime.Now;

            int rowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "Update Applications SET ApplicationStatus = @ApplicationStatus, LastStatusDate = @LastStatusDate WHERE ApplicationID = @ApplicationID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                        command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);

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

        static public bool UpdateLicenseClass(int ApplicationID, int LicenseClassID)
        {
            

            int rowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "Update Applications SET LicenseClassID = @LicenseClassID WHERE ApplicationID = @ApplicationID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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

        static public DataTable GetAllApplications()
        {
            DataTable dtAllApplication = new DataTable();
  

            try           
            {

                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Applications";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) { dtAllApplication.Load(reader); }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtAllApplication;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();
                        object result = command.ExecuteScalar();


                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
                return ActiveApplicationID;
            }


            return ActiveApplicationID;
        }

        public static bool FindApplication(int ApplicationID, ref int ApplicantPersonID, ref int ApplicationTypeID, ref int LicenseClassID, ref int CreatedByUserID,
                                              ref Decimal PaidFees, ref byte ApplicationStatus, ref DateTime ApplicationDate, ref DateTime LastStatusDate)
        {
            bool isFound = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            //Application was found
                            isFound = true;
                            ApplicationTypeID = (int)reader["ApplicationTypeID"];
                            PaidFees = (decimal)reader["PaidFees"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            LicenseClassID = (int)reader["LicenseClassID"];
                            ApplicantPersonID = (int)reader["ApplicantPersonID"];
                            ApplicationStatus = (byte)reader["ApplicationStatus"];
                            ApplicationDate = (DateTime)reader["ApplicationDate"];
                            LastStatusDate = (DateTime)reader["LastStatusDate"];
                        }
                        else
                        {
                            //Application was not found
                            isFound = false;
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
        public static bool DeleteApplication(int ApplicationID)
        {          
            int rowAffected = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";
                    connection.Open();

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
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
    }
}


