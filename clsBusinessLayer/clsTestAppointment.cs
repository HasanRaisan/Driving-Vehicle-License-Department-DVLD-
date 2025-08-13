using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
    public class clsTestAppointment
    {


        public int TestAppointmetID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; } 
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool isLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        public clsApplication RetakeTestAppInfo;
        public clsTestType.enTestType TestTypeID { set; get; }

        private enum enMode { AddNew = 1, Update = 2 }
        private enMode Status { get; set; }

        public clsTestAppointment()
        {
            this.Status = enMode.AddNew;
            this.TestAppointmetID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.MinValue;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            RetakeTestAppInfo = null;
            this.isLocked = false;
        }

        clsTestAppointment(int TestAppointmetID, clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID) 
        {
            this.Status = enMode.Update;
            this.TestAppointmetID = TestAppointmetID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.RetakeTestApplicationID= RetakeTestApplicationID;
             if (RetakeTestApplicationID != -1) this.RetakeTestAppInfo = clsApplication.FindApplication(RetakeTestApplicationID);
            this.isLocked = IsLocked;
        }


        static public clsTestAppointment FindTestAppointment(int TestAppointmetID)
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
                return new clsTestAppointment(TestAppointmetID, (clsTestType.enTestType) TestTypeID, LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked, RetakeTestApplicationID);
            }
            else return null;

        }

        private bool _UpdateTestAppointmentDate()
        {
            return clsTestAppointmetsDataAccess.UpdateTestAppointmentDate(this.TestAppointmetID, this.AppointmentDate);
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
            this.TestAppointmetID = clsTestAppointmetsDataAccess.AddNewApointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,this.AppointmentDate,this.PaidFees, this.CreatedByUserID);

            return (this.TestAppointmetID != -1);
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
