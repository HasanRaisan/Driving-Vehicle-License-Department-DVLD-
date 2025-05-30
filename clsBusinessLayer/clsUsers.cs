using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;

namespace BusinessLayer
{
    public class clsUsers
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PersonID { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo { get; set; }

        private enMode _Status { get; set; }
        private enum enMode { AddNew = 0, Update = 1 };


        public clsUsers() { _Status = enMode.AddNew;
            this.UserName = "";
            this.Password = "";
         
        }

        clsUsers(int UseerID , string UserName, string Password, int PersonID , bool IsActive)
        {
            this.UserID = UseerID;
            this.UserName = UserName;
            this.Password = Password;
            this.PersonID = PersonID;
            this.IsActive = IsActive;
            this.PersonInfo = clsPerson.FindPerson(PersonID);
            _Status = enMode.Update;
        }


        static public clsUsers FindUser(int UserID)
        {
            

            if (clsUserDataAccess.IsUserExist(UserID))
            {

                string UserName = string.Empty;
                string Password = string.Empty;
                int PersonID = 0;
                bool IsActive = false;

                clsUserDataAccess.FindUser(UserID, ref UserName, ref Password, ref PersonID, ref IsActive);

                return new clsUsers(UserID, UserName,Password,PersonID,IsActive);

            }
            else return null;
        }

        static public clsUsers FindUser(string UserName)
        {

            if (clsUserDataAccess.IsUserExist(UserName))
            {

                int UserID = 0;
                string Password = string.Empty;
                int PersonID = 0;
                bool IsActive = false;


                clsUserDataAccess.FindUser(UserName, ref UserID, ref Password, ref PersonID, ref IsActive);

                return new clsUsers(UserID,  UserName, Password, PersonID, IsActive);

            }
            else { return null; }
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserDataAccess.AddUser(this.UserName,this.Password,this.PersonID,this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            
            return clsUserDataAccess.UpdateUser(this.UserID,this.UserName,this.Password,this.PersonID, this.IsActive);
        }

        static public bool IsUserExist(int UserID)
        {
            return clsUserDataAccess.IsUserExist(UserID);
        }

        static public bool IsUserExist(string UserName)
        {
            return clsUserDataAccess.IsUserExist(UserName);
        }

        static public bool IsUserExistByPersonID(int PersonID)
        {
            return clsUserDataAccess.IsUserExistByPersonID(PersonID);
        }

        static public DataTable GetUsers()
        {
            return clsUserDataAccess.GetUsers();
        }

        static public bool DeleteUser(int UserID)
        {
            return clsUserDataAccess.DeleteUser(UserID);
        }

        public bool Save()
        {
            switch (this._Status)
            {
                case enMode.AddNew: 

                    if (_AddNewUser())
                    {
                        this._Status = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                case enMode.Update: return _UpdateUser();

                default: return false;

            }

        }



        public static int GetUserIDByUserName(string Username)
        {
            return clsUserDataAccess.GetIDByUserName(Username);
        }


        //// If the column is NOT NULL
        //bool isActive = (bool)reader["IsActive"];

        //// If the column allows NULL
        //bool? isActiveNullable = reader["IsActive"] as bool?;




    }
}
