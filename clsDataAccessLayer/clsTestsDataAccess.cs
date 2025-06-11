using clsDataAccessLayer;
using ConnectionDataBaseLincense;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsTestsDataAccess
    {
        /*
        Tests
        TestAppointmentID
        TestResult
        Notes
        CreatedByUserID
         
         */


        static public int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            int TestID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("AddNewTest", connection))
                    {
                       command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        command.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? DBNull.Value : (object)Notes);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@TestResult", TestResult);

                        SqlParameter outPutID = new SqlParameter("@NewTestID", System.Data.SqlDbType.Int)
                        { Direction = ParameterDirection.Output };

                        command.Parameters.Add(outPutID);

                        connection.Open();

                        command.ExecuteNonQuery();

                        TestID = (outPutID.Value != DBNull.Value) ? Convert.ToInt32(outPutID.Value) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return TestID;

        }

        static public bool FindTest(int TestAppointmentID, ref int TestID  , ref bool TestResult , ref string Notes)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "  SELECT * FROM Tests Where Tests.TestAppointmentID = @TestAppointmentID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;
                            TestID = (int)reader["TestID"];
                            TestResult = (bool)reader["TestResult"];
                            Notes = (string)reader["Notes"];
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
    }
}
