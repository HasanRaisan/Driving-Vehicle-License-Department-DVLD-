using ConnectionDataBaseLincense;
using System;
using System.Data;
using System.Data.SqlClient;


namespace clsDataAccessLayer
{
    public class clsDriverDataAccess
    {


        /*
         Drivers
         
         DriverID
         PersonID
         CreatedByUserID
         CreatedDate   
         */


        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "INSERT INTO Drivers (PersonID, CreatedByUserID) " +
                                   "VALUES (@PersonID, @CreatedByUserID); " +
                                   "SELECT SCOPE_IDENTITY();";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            DriverID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }


            return DriverID;
        }

        static public bool FindDriverByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        { bool isFound = false;
            try

            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriverID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // Driver was found
                            isFound = true;
                            PersonID = (int)reader["PersonID"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];

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

        static public bool FindDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // Record found
                            isFound = true;
                            DriverID = (int)reader["DriverID"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];
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

        static public bool DoesDriverExistByDriverID(int DriverID)
        {
            bool isExist = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "SELECT 1 FROM Drivers WHERE DriverID = @DriverID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriverID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        isExist = reader.HasRows;


                        //if(reader.Read())
                        // isExist = true;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }


            return isExist;
        }

        static public bool DoesDriverExistByPersonID(int PersonID)
        {
            bool isExist = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = "SELECT 1 FROM Drivers WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        isExist = reader.HasRows;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }


            return isExist;
        }

        static public DataTable GetAllDriversWithDetails()
        {
            DataTable dtDrivers = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT 
                               Drivers.DriverID AS 'Driver ID',
                               People.PersonID AS 'Person ID',
                               People.NationalNo AS 'National NO.',
                               (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) AS 'Full Name',
                               Drivers.CreatedDate AS 'Created Date',
                               COUNT(CAST(Licenses.IsActive AS int)) AS 'Active Licenses'
                           FROM 
                               Drivers 
                           INNER JOIN 
                               People ON Drivers.PersonID = People.PersonID 
                           INNER JOIN 
                               Licenses ON Licenses.DriverID = Drivers.DriverID
                           	Where Licenses.IsActive = 1
                           GROUP BY 
                               Drivers.DriverID, 
                               People.PersonID, 
                               People.NationalNo, 
                               People.FirstName, 
                               People.SecondName, 
                               People.ThirdName, 
                               People.LastName, 
                               Drivers.CreatedDate
                            ORDER BY Drivers.CreatedDate DESC;";



                    /*
                     SUBQUERY. THE GROUP BY BETTER IN LARG DATA


                        SELECT 
            Drivers.DriverID AS 'Driver ID',
            People.PersonID AS 'Person ID',
            People.NationalNo AS 'National NO.',
            (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) AS 'Full Name',
            Drivers.CreatedDate AS 'Created Date',
            (Select Count(Licenses.LicenseID) From  Licenses Where (IsActive = 1 )  AND Licenses.DriverID = Drivers.DriverID  ) AS 'Active License'	 
        FROM 
            Drivers 
        INNER JOIN 
            People ON Drivers.PersonID = People.PersonID 

                            */

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        dtDrivers.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtDrivers;
        }

    }
}
