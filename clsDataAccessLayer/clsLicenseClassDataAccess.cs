using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clsDataAccessLayer;
using ConnectionDataBaseLincense;

namespace DataAccessLayer
{
    public class clsLicenseClassDataAccess
    {
        static public DataTable GetAllLicenseClasses()
        {

            DataTable dtAllClasses = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "SELECT * FROM LicenseClasses";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                         dtAllClasses.Load(reader);
                        reader.Close();
                    }
                }
            }
            catch (Exception ex) { clsGlobalDataAccess.LogError(ex); }

            return dtAllClasses;
        }



        public static bool FindLicenseClassByID(int LicenseClassID, ref string ClassName, ref string ClassDescription,
                                                ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref decimal ClassFees)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;
                            ClassName = reader["ClassName"].ToString();
                            ClassDescription = reader["ClassDescription"].ToString();
                            MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                            DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                            ClassFees = (decimal)reader["ClassFees"];
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



        public static bool FindLicenseClassByName(string ClassNameToSearch, ref int LicenseClassID, ref string ClassName,
                                          ref string ClassDescription, ref byte MinimumAllowedAge,
                                          ref byte DefaultValidityLength, ref decimal ClassFees)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", ClassNameToSearch);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;
                            LicenseClassID = (int)reader["LicenseClassID"];
                            ClassName = reader["ClassName"].ToString();
                            ClassDescription = reader["ClassDescription"].ToString();
                            MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                            DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                            ClassFees = (decimal)reader["ClassFees"];
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
