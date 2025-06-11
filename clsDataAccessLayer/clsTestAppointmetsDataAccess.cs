using clsDataAccessLayer;
using ConnectionDataBaseLincense;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestAppointmetsDataAccess
    {
        /*
          

        TestTypeID
        LocalDrivingLicenseApplicationID
        AppointmentDate
        PaidFees
        CreatedByUserID
        IsLocked
         SELECT SCOPE_IDENTITY();
         */

        static public bool UpdateTestAppointmentDate(int TestAppointmentID,DateTime AppointmentDate)
        {
            int rowAffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "Update TestAppointments SET AppointmentDate = @AppointmentDate  WHERE TestAppointmentID = @TestAppointmentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);



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

        // Not used -- moved to add new test id 
        static public bool LocckedTestAppointment(int TestAppointmentID)
        {
            bool IsLocked = true;

            int rowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDataBaseLincense.clsDataAccessSettings.ConnectionString))
                {
                    string query = "Update TestAppointments SET IsLocked = @IsLocked  WHERE TestAppointmentID = @TestAppointmentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        command.Parameters.AddWithValue("@IsLocked", IsLocked);



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

        static public bool DoseLocalDrivingLicenseHaveAnActiveAppointment(int LocalDrivingLicenseApplicationID)
        {
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "SELECT 1 FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND IsLocked = 0";

                    using (var command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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
        
        static public DataTable GetAllApplicationAppointment(int LocalDrivingLicenseApplicationID)
        {
            var dtAppointments = new DataTable();

            try
            {

                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = @"SELECT TestAppointments.TestAppointmentID AS 'Test Appointment ID',
                                   TestAppointments.AppointmentDate AS 'Appointment Date',
                            	   TestAppointments.PaidFees AS 'Paid Fees',
                            	   Case TestAppointments.IsLocked 
                            	      WHEN 1 THEN 'Yes'
                            		  WHEN 0 THEN 'No'
                            		  END AS 'Is Locked'            	  
                            FROM TestAppointments
                            Where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID; ";

                    using (var command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                            dtAppointments.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtAppointments;
        }

        static public int AddNewApointment( int TestTypeID,  int LocalDrivingLicenseApplicationID,   DateTime AppointmentDate,
                                            decimal PaidFees ,  int CreatedByUserID )
        {
            bool IsLocked = false;
            int AppointmetID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate," +
                                   "PaidFees, CreatedByUserID, IsLocked) VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID," +
                                   "@AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked) SELECT SCOPE_IDENTITY(); ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@IsLocked", IsLocked);


                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int resultID))
                        {
                            AppointmetID = resultID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return AppointmetID;

        }

        static public bool FindAppointment(int TestAppointmentID, ref int TestTypeID , ref int LocalDrivingLicenseApplicationID,ref DateTime AppointmentDate,
                                            ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked , ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            //Test Appointment was found
                            isFound = true;
                            TestTypeID = (int)reader["TestTypeID"];
                            PaidFees = (decimal)reader["PaidFees"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                            IsLocked = (bool)reader["IsLocked"];
                            AppointmentDate = (DateTime)reader["AppointmentDate"];
                            RetakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value ? (int)reader["RetakeTestApplicationID"] : -1;
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

    }
}
