using clsDataAccessLayer;
using ConnectionDataBaseLincense;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    string query = "INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) VALUES" +
                                   "(@TestAppointmentID ,@TestResult, @Notes, @CreatedByUserID) SELECT SCOPE_IDENTITY(); ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                        if (string.IsNullOrEmpty(Notes))
                            command.Parameters.AddWithValue("@Notes", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Notes", Notes);

                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@TestResult", TestResult);


                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int resultID))
                        {
                            TestID = resultID;
                        }

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
