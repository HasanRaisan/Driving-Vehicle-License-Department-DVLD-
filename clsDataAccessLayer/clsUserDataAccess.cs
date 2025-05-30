using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Reflection;
using clsDataAccessLayer;
using ConnectionDataBaseLincense;

namespace DataAccessLayer
{
    public class clsUserDataAccess
    {
        static public bool FindUser(int UserID, ref string UserName, ref string Password ,ref int PersonID, ref bool IsActive )
        {
            bool idFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Users WHERE UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            //User was found
                            idFound = true;
                            UserName = (string)reader["UserName"];
                            Password = (string)reader["Password"];
                            PersonID = (int)reader["PersonID"];
                            IsActive = (bool)reader["IsActive"];
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return idFound;
        }

        static public bool FindUser(string UserName, ref int UserID, ref string Password, ref int PersonID, ref bool IsActive)
        {
            bool idFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Users WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            //User was found
                            idFound = true;
                            UserID = (int)reader["UserID"];
                            Password = (string)reader["Password"];
                            PersonID = (int)reader["PersonID"];
                            IsActive = (bool)reader["IsActive"];
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return idFound;
        }

        static public int GetIDByUserName(string UserName)
        {
            int UserID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT UserID FROM Users WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {

                            UserID = (int)reader["UserID"];
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return UserID;
        }

        static public bool UpdateUser(int UserID, string UserName,  string Password,  int PersonID,  bool IsActive)
        {
            int rowAffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "Update Users Set Password = @Password , UserName = @UserName , IsActive = @IsActive , PersonID = @PersonID Where UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
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

        static public int AddUser( string UserName, string Password,  int PersonID, bool IsActive)
        {

            int UserID = -1;

            try
  {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "INSERT INTO  [Users] (Password, UserName, IsActive, PersonID)  Values (@Password, @UserName, @IsActive, @PersonID) SELECT SCOPE_IDENTITY();";

                    //this function will return the new perosn id if succeeded and -1 if not.

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            UserID = insertedID;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return UserID;
        }

        static public bool DeleteUser(int UserID)
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "DELETE from Users WHERE UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
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

        static public bool IsUserExist(int UserID)
        {
            bool isExist = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT 1 FROM Users WHERE UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(@query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
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

        static public bool IsUserExistByPersonID(int PersonID)
        {
            bool isExist = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT 1 FROM Users WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(@query, connection))
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

        static public bool IsUserExist(string UserName)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT 1 FROM Users WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(@query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
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


        static public DataTable GetUsers()
        {
            DataTable dtUsers = new DataTable();


            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @" SELECT UserID AS 'User ID', PersonID AS 'Person ID', UserName AS 'Username',
              case 
                   WHEN IsActive = 1 THEN 'YES' 
              	 WHEN IsActive = 0 THEN 'NO' 
              END  AS 'Is Active'
              FROM   Users ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dtUsers.Load(reader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }

            return dtUsers;
        }

        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password,
                                ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Users WHERE Username = @Username and Password=@Password;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", UserName);
                        command.Parameters.AddWithValue("@Password", Password);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;
                            UserID = (int)reader["UserID"];
                            PersonID = (int)reader["PersonID"];
                            UserName = (string)reader["UserName"];
                            Password = (string)reader["Password"];
                            IsActive = (bool)reader["IsActive"];
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

        public static bool ChangePassword(int UserID, string NewPassword)
        {

            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = @"Update  Users  
                            set Password = @Password
                            where UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        command.Parameters.AddWithValue("@UserID", UserID);


                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalDataAccess.LogError(ex);
            }
            return (rowsAffected > 0);
        }

    }
}
