using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer
{
     public class clsLocalDrivingLicensesApplication : clsApplication
     {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLicenseClasses LicenseClassInfo { get; set; }
        public clsPerson PersonInfo { get; set; }





        public clsLocalDrivingLicensesApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }

        clsLocalDrivingLicensesApplication(int ApplicationID, int LDLAppID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             decimal PaidFees, int CreatedByUserID, int LicenseClassID) 
        {
            this.LocalDrivingLicenseApplicationID = LDLAppID;
            this.LicenseClassID = LicenseClassID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;

            this.Mode = enMode.Update;

            this.LicenseClassInfo = clsLicenseClasses.FindLicenseClass(LicenseClassID);
            this.PersonInfo = clsPerson.FindPerson(clsApplication.FindApplication(ApplicationID).ApplicantPersonID);
        }


        // Should Use clsLocalDrivingLicensesApplication (value) : base (value)
         
        /*
         
                   public clsLocalDrivingLicensesApplication(
             int ApplicationID,
             int LDLAppID,
             int ApplicantPersonID,
             DateTime ApplicationDate,
             int ApplicationTypeID,
             enApplicationStatus ApplicationStatus,
             DateTime LastStatusDate,
             decimal PaidFees,
             int CreatedByUserID,
             int LicenseClassID
         ) 
         : base(ApplicationID, ApplicantPersonID, ApplicationTypeID, LicenseClassID,
                CreatedByUserID, PaidFees, ApplicationStatus, ApplicationDate, LastStatusDate)
         {
             this.LocalDrivingLicenseApplicationID = LDLAppID;
             this.LicenseClassInfo = clsLicenseClasses.FindLicenseClass(LicenseClassID);
             this.PersonInfo = clsPerson.FindPerson(this.ApplicantPersonID);
         }

         
         */


        private bool _AddNewLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsDataAccess.AddNewLocalDrivingLicenseApplications(this.ApplicationID, this.LicenseClassID);

            return this.LocalDrivingLicenseApplicationID != -1;
        }

        static public clsLocalDrivingLicensesApplication FindLocalDrvingLicenseAppByBaseAppID(int ApplicationID)
        {
            int LDLAppID = 0;
            int LicenseClassID = 0;

            // find the local driving license application by the base application ID
            if ( clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref LDLAppID, ref LicenseClassID))
            {
                // then we find the base application
                clsApplication Application = clsApplication.FindApplication(ApplicationID);

                return new clsLocalDrivingLicensesApplication(ApplicationID, LDLAppID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus,
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);

            }
            else return null;


        }

        static public clsLocalDrivingLicensesApplication FindLocalDrvingLicenseAppByID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = 0;
            int LicenseClassID = 0;

            // find the local driving license application by it's ID
            if (clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                // then we find the base application
                clsApplication Application = clsApplication.FindApplication(ApplicationID);
                return new clsLocalDrivingLicensesApplication(ApplicationID, LocalDrivingLicenseApplicationID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus,
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);

            }
            else return null;


        }


        public bool DoesAttendTestType(int TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }


        static public DataTable GeLAllLocalDrivingLicenseApplicationsForMange()
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.GeLAllLocalDrivingLicenseApplicationsForMange();
        }


        public bool UpdateLicenseClass()
        {
           if(clsApplicationsDataAccess.UpdateLicenseClass(this.ApplicationID, this.LicenseClassID))
            return clsLocalDrivingLicenseApplicationsDataAccess.UpdateLicenseClass(this.LocalDrivingLicenseApplicationID, this.LicenseClassID);
           return false;
        }

        public bool Delete()
        {

            bool isLocalDrivingApplicationDeleted = false;
            bool isBaseApplicationDeleted = false;
            // We delete the local driving license application first
            isLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationsDataAccess.DeleteLocalDrivingLicense(this.LocalDrivingLicenseApplicationID);

            if (!isLocalDrivingApplicationDeleted)
                return false;

            // then delete the base application
            isBaseApplicationDeleted = base.DeleteApplication();
            return isBaseApplicationDeleted;
        }

        static public bool DidPassThisTestType(int LocalDrivingLicenseAppID, int TestTypeID)
        {
          return  clsLocalDrivingLicenseApplicationsDataAccess.DoseLocalDrvingLicenseApplicationPassThisTest(LocalDrivingLicenseAppID, TestTypeID);
        }

        public bool DidPassThisTestType(int TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoseLocalDrvingLicenseApplicationPassThisTest(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool PassedAllTests()
        {
           
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public int GetActiveLicenseID()
        {
            //this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindDriverByPersonID(this.PersonInfo.PersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();

                Driver.PersonID = this.PersonInfo.PersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
           
            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                 this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.

            base.Mode = (clsApplication.enMode)this.Mode; // set the mode in the base class to be the same as this one
            if (!base.Save())
                return false;



            //After we save the main application now we save the sub application.
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplications())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //case enMode.Update:

                    //return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

     }
}
