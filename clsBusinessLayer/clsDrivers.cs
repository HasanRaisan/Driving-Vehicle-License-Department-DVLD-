using clsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsDrivers
    {
        public int _DriverID { get; set; }
        public int _PersonID { get; set; }
        public int _CreatedByUserID { get; set; }
        public DateTime _CreatedDate { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsDrivers() { }

        private clsDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this._DriverID = DriverID;
            this._PersonID = PersonID;
            this._CreatedByUserID = CreatedByUserID;
            this._CreatedDate = CreatedDate;
            this.PersonInfo = clsPerson.FindPerson(PersonID);
        }

        static public clsDrivers FindDriver(int DriverID)
        {
            if (clsDriverDataAccess.DoesDriverExistByDriverID(DriverID))
            {
                int PersonID = int.MinValue;
                int CreatedByUserID = int.MinValue;
                DateTime CreatedDate = DateTime.Now;
                clsDriverDataAccess.FindDriverByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }


        static public clsDrivers FindDriverByPersonID(int PersonID)
        {
            if (clsDriverDataAccess.DoesDriverExistByPersonID(PersonID))
            {
                int DriverID = int.MinValue;
                int CreatedByUserID = int.MinValue;
                DateTime CreatedDate = DateTime.Now;
                clsDriverDataAccess.FindDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate);
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }






        private bool _AddNewDriver()
        {
            this._DriverID = clsDriverDataAccess.AddNewDriver(this._PersonID, this._CreatedByUserID);
            return (this._DriverID != -1);
        }

        public bool Save()
        {
            return this._AddNewDriver();
        }

        static public bool IsDriverExist(int DriverID)
        {
            return clsDriverDataAccess.DoesDriverExistByDriverID(DriverID);
        }


        public static DataTable GetAllDrivers()
        {
            return clsDriverDataAccess.GetAllDriversWithDetails();
        }
    }

}
