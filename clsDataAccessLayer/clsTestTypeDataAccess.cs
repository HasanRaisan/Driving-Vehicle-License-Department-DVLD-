using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConnectionDataBaseLincense;
using clsDataAccessLayer;

namespace DataAccessLayer
{
    public class clsTestTypeDataAccess
    {


        static public DataTable GetAllTestType()
        {
            DataTable dtTestType = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "SELECT * FROM TestTypes";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                            dtTestType.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtTestType;
        }

       static public bool FindTestType(int TestTypeID, ref decimal TestTypeFees, ref string TestTypeTitle, ref string
             TestTypeDescription)
        {

            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {

                            isFound = true;
                            TestTypeFees = (decimal)reader["TestTypeFees"];
                            TestTypeTitle = reader["TestTypeTitle"].ToString();
                            TestTypeDescription = reader["TestTypeDescription"].ToString();

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

        static public bool UpdateTestType(int TestTypeID, decimal TestTypeFees, string TestTypeTitle, string TestTypeDescription)
        {
            int rowAffected = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "UPDATE TestTypes SET TestTypeFees = @TestTypeFees, TestTypeTitle = @TestTypeTitle, TestTypeDescription = @TestTypeDescription  WHERE TestTypeID = @TestTypeID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
                        command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                        command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);

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

        [Obsolete("⚠️ Warning: This method is not yet implemented. It will only return -1.", false)]
        static public int AddNewTestType(string Title, string Description, decimal Fees)
        {
            throw new NotImplementedException("⚠️ Warning: This method is not yet implemented. It will only return -1.");
            // not implemented  yet
        }
    }
}
