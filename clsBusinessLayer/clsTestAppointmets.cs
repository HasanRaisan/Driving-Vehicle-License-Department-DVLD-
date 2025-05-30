using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;

namespace BusinessLayer
{
    public class clsTestAppointmets
    {
        public int _TestAppointmetID { get; set; }
        public int _TestTypeID { get; set; }
        public int _LocalDrivingLicenseApplicationID { get; set; } 
        public DateTime _AppointmentDate { get; set; }
        public decimal _PaidFees { get; set; }
        public int _CreatedByUserID { get; set; }
        public bool isLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplications RetakeTestAppInfo;

        private enum enMode { AddNew = 1, Update = 2 }
        private enMode Status { get; set; }

        public clsTestAppointmets()
        {
            this.Status = enMode.AddNew;
            this._TestAppointmetID = -1;
            this._TestTypeID = -1;
            this._LocalDrivingLicenseApplicationID = -1;
            this._AppointmentDate = DateTime.MinValue;
            this._PaidFees = 0;
            this._CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            RetakeTestAppInfo = null;
            this.isLocked = false;
        }

        clsTestAppointmets(int TestAppointmetID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID) 
        {
            this.Status = enMode.Update;
            this._TestAppointmetID = TestAppointmetID;
            this._TestTypeID = TestTypeID;
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this._AppointmentDate = AppointmentDate;
            this._PaidFees = PaidFees;
            this._CreatedByUserID = CreatedByUserID;
            this.RetakeTestApplicationID= RetakeTestApplicationID;
             if (RetakeTestApplicationID != -1) this.RetakeTestAppInfo = clsApplications.FindApplication(RetakeTestApplicationID);
            this.isLocked = IsLocked;
        }


        static public clsTestAppointmets FindTestAppointment(int TestAppointmetID)
        {
            int TestTypeID = int.MinValue;
            int LocalDrivingLicenseApplicationID = int.MinValue;
            DateTime AppointmentDate = DateTime.MinValue;
            decimal PaidFees = int.MinValue;
            int CreatedByUserID = int.MinValue;
            int RetakeTestApplicationID = int.MinValue;
            bool IsLocked = false;

            if (clsTestAppointmetsDataAccess.FindAppointment(TestAppointmetID,ref TestTypeID,ref LocalDrivingLicenseApplicationID,ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointmets(TestAppointmetID,TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked, RetakeTestApplicationID);
            }
            else return null;

        }

        private bool _UpdateTestAppointmentDate()
        {
            return clsTestAppointmetsDataAccess.UpdateTestAppointmentDate(this._TestAppointmetID, this._AppointmentDate);
        }


        static public bool LockedTestAppointment(int TestAppointmetID)
        {
            return clsTestAppointmetsDataAccess.LocckedTestAppointment(TestAppointmetID);
        }

        static public bool DoseLocalDrivingLicenseHaveAnActiveAppointment(int LocalDrivingLicenseApplicationID)
        {
         return  clsTestAppointmetsDataAccess.DoseLocalDrivingLicenseHaveAnActiveAppointment(LocalDrivingLicenseApplicationID);
        }

        static public DataTable GetAllApplicationAppointments(int GetAllApplicationAppointment)
        {
            return clsTestAppointmetsDataAccess.GetAllApplicationAppointment(GetAllApplicationAppointment);
        }

        private bool _AddNewAppointment()
        {
            this._TestAppointmetID = clsTestAppointmetsDataAccess.AddNewApointment(this._TestTypeID, this._LocalDrivingLicenseApplicationID,this._AppointmentDate,this._PaidFees, this._CreatedByUserID);

            return (this._TestAppointmetID != -1);
        }

        public bool Save()
        {
            switch (this.Status)
            {
                case enMode.Update:
                    return _UpdateTestAppointmentDate();

                case enMode.AddNew:
                    if (_AddNewAppointment())
                    {
                        this.Status = enMode.Update;
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
    }
}
