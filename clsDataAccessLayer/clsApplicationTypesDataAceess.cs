


using clsDataAccessLayer;
using ConnectionDataBaseLincense;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessLayer
{
   
    public class clsApplicationTypesDataAceess
    {
        static public DataTable GetAllApplicationTypes()
        {
            DataTable dtApplicationTypes = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM ApplicationTypes";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {



                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                            dtApplicationTypes.Load(reader);

                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtApplicationTypes;

        }

        static public bool FindApplication(int ApplicationTypeID, ref decimal ApplicationFees,  ref string ApplicationTypeTitle)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            ApplicationFees = (decimal)reader["ApplicationFees"];
                            ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                            isFound = true;
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

        static public bool UpdateApplicationType(int ApplicationTypeID, decimal ApplicationFees, string ApplicationTypeTitle)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "UPDATE ApplicationTypes SET ApplicationFees = @ApplicationFees, ApplicationTypeTitle = @ApplicationTypeTitle WHERE ApplicationTypeID = @ApplicationTypeID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
                        command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);

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
        static public int AddApplicationType(decimal ApplicationFees, string ApplicationTypeTitle)
        {
            // Not implemented  yet
            throw new NotImplementedException("⚠️ Warning: This method is not yet implemented. It will only return -1.");
        }
    }
}
