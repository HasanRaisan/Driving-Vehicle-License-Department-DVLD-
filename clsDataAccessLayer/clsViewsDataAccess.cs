using clsDataAccessLayer;
using ConnectionDataBaseLincense;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsViewsDataAccess
    {


        static public bool FindLDLApp_View(int LocalDrivingLicenseApplicationID, ref string ClassName, ref string NationalNo, ref string FullName, 
                                             ref DateTime ApplicationDate, ref int PassedTestCount, ref string Status, ref int BaseApplicationID)
        {
            bool isFound = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM LocalDrivingLicenseApplications_View WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // application was found

                            isFound = true;
                            ClassName = (string)reader["ClassName"];
                            NationalNo = (string)reader["NationalNo"];
                            FullName = (string)reader["FullName"];
                            ApplicationDate = (DateTime)reader["ApplicationDate"];
                            PassedTestCount = (int)reader["PassedTestCount"];
                            Status = (string)reader["Status"];
                            BaseApplicationID = (int)reader["BaseApplicationID"];

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




        public static bool FindLicenseDetailsViewByLicenseID(int LicenseID, ref string ClassName, ref string FullName, ref string NationalNo,
                                                              ref string Gender, ref DateTime IssueDate, ref byte IssueReason,
                                                              ref string Notes, ref string IsActive, ref DateTime DateOfBirth,
                                                              ref DateTime ExpirationDate, ref string IsDetained, ref int DriverID)
        {
            bool isFound = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM LicenseDetails_View WHERE LicenseID = @LicenseID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            ClassName = reader["ClassName"].ToString();
                            FullName = reader["FullName"].ToString();
                            NationalNo = reader["NationalNo"].ToString();
                            Gender = reader["Gender"].ToString();
                            IssueDate = (DateTime)reader["IssueDate"];
                            IssueReason = (byte)reader["IssueReason"];
                            Notes = reader["Notes"].ToString();
                            IsActive = reader["IsActive"].ToString();
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            ExpirationDate = (DateTime)reader["ExpirationDate"];
                            IsDetained = reader["IsDeatin"].ToString();
                            DriverID = (int)reader["DriverID"];
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
