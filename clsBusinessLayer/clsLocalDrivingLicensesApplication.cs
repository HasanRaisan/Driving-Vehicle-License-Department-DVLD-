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
     public class clsLocalDrivingLicensesApplication
     {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public int BaseApplicationID { get; set; }
        public clsLicenseClasses LicenseClassInfo { get; set; }
        public clsPerson PersonInfo { get; set; }



        private bool _AddNewLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsDataAccess.AddNewLocalDrivingLicenseApplications(this.BaseApplicationID, this.LicenseClassID);

            return this.LocalDrivingLicenseApplicationID != -1;
        }

       public clsLocalDrivingLicensesApplication()
       {

       }
        clsLocalDrivingLicensesApplication(int ApplicationID, int LDLAppID, int LicenseClassID)
        {
           this.LocalDrivingLicenseApplicationID = LDLAppID;
            this.LicenseClassID = LicenseClassID;
            this.BaseApplicationID = ApplicationID;
            this.LicenseClassInfo = clsLicenseClasses.FindLicenseClass(LicenseClassID);
            this.PersonInfo = clsPerson.FindPerson(clsApplications.FindApplication(ApplicationID)._ApplicantPersonID);
        }

        static public clsLocalDrivingLicensesApplication FindLocalDrvingLicenseAppByBaseAppID(int ApplicationID)
        {
            int LDLAppID = 0;
            int LicenseClassID = 0;

           if( clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref LDLAppID, ref LicenseClassID))
           {
                return new clsLocalDrivingLicensesApplication(ApplicationID  , LDLAppID , LicenseClassID);
           }
           else return null;


        }

        static public clsLocalDrivingLicensesApplication FindLocalDrvingLicenseAppByID(int LDLAppID)
        {
            int ApplicationID = 0;
            int LicenseClassID = 0;

            if (clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationInfoByID(LDLAppID, ref ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicensesApplication(ApplicationID, LDLAppID, LicenseClassID);
            }
            else return null;


        }
        public bool DoesAttendTestType(int TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool Save()
        {
            return _AddNewLocalDrivingLicenseApplications();
        }

        static public DataTable GeLAllLocalDrivingLicenseApplicationsForMange()
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.GeLAllLocalDrivingLicenseApplicationsForMange();
        }


        public bool UpdateLicenseClass()
        {
           if(clsApplicationsDataAccess.UpdateLicenseClass(this.BaseApplicationID, this.LicenseClassID))
            return clsLocalDrivingLicenseApplicationsDataAccess.UpdateLicenseClass(this.LocalDrivingLicenseApplicationID, this.LicenseClassID);
           return false;
        }

        static public bool DeleteLocalDrivingLicense(int LDLAppID)
        {
            int ApplicationID = FindLocalDrvingLicenseAppByID(LDLAppID).BaseApplicationID;
            bool deletingResult = clsLocalDrivingLicenseApplicationsDataAccess.DeleteLocalDrivingLicense(LDLAppID);

            // Delete basic application 
            clsApplicationsDataAccess.DeleteApplication(ApplicationID);
            return deletingResult;
        }

        static public bool DidPassThisTestType(int LocalDrivingLicenseAppID, int TestTypeID)
        {
          return  clsLocalDrivingLicenseApplicationsDataAccess.DoseLocalDrvingLicenseApplicationPassThisTest(LocalDrivingLicenseAppID, TestTypeID);
        }

        public bool DidPassThisTestType(int TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoseLocalDrvingLicenseApplicationPassThisTest(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDrivers Driver = clsDrivers.FindDriverByPersonID(this.PersonInfo._PersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDrivers();

                Driver._PersonID = this.PersonInfo._PersonID;
                Driver._CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver._DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver._DriverID;
            }
           
            clsLicenses License = new clsLicenses();
            License._ApplicationID = this.BaseApplicationID;
            License._DriverID = DriverID;
            License._LicenseClassID = this.LicenseClassID;
            License._ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License._Notes = Notes;
            License._PaidFees = this.LicenseClassInfo.ClassFees;
            License._IsActive = true;
            License.IssueReason = clsLicenses.enIssueReason.FirstTime;
            License._CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
 //               this.SetComplete();

                return License._LicenseID;
            }

            else
                return -1;
        }


    }
}
