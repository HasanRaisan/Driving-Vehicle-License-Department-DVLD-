using clsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsDriver
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsDriver() { }

        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPerson.FindPerson(PersonID);
        }

        static public clsDriver FindDriver(int DriverID)
        {
            if (clsDriverDataAccess.DoesDriverExistByDriverID(DriverID))
            {
                int PersonID = int.MinValue;
                int CreatedByUserID = int.MinValue;
                DateTime CreatedDate = DateTime.Now;
                clsDriverDataAccess.FindDriverByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }


        static public clsDriver FindDriverByPersonID(int PersonID)
        {
            if (clsDriverDataAccess.DoesDriverExistByPersonID(PersonID))
            {
                int DriverID = int.MinValue;
                int CreatedByUserID = int.MinValue;
                DateTime CreatedDate = DateTime.Now;
                clsDriverDataAccess.FindDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate);
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }






        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID);
            return (this.DriverID != -1);
        }




        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.Update:
                //return ();

                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return false;
            }
        }


        public static DataTable GetAllDrivers()
        {
            return clsDriverDataAccess.GetAllDriversWithDetails();
        }
    }

}
