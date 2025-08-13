using clsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; private set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public clsDriver DriverInfo;


        public clsInternationalLicense() 
        {
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;

            Mode = enMode.AddNew;
        }   
        private clsInternationalLicense(int licenseID, int ApplicationID, int driverID, int issuedUsingLocalLicenseID,
                                                    DateTime issueDate, DateTime expirationDate, bool isActive,
                                        int ApplicantPersonID, DateTime ApplicationDate, enApplicationStatus ApplicationStatus, 
                                        DateTime LastStatusDate, Decimal PaidFees, int CreatedByUserID)
        {
            // base constructor
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            InternationalLicenseID = licenseID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;

            this.DriverInfo = clsDriver.FindDriver(this.DriverID);
        }

        private bool _AddNewLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseDataAccess.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                                                                                this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);

        }

        public static clsInternationalLicense FindLicenseByID(int licenseID)
        {
            int applicationID = 0;
            int driverID = 0;
            int issuedUsingLocalLicenseID = 0;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            bool isActive = false;
            int createdByUserID = 0;

            bool isFound = clsInternationalLicenseDataAccess.FindInternationalLicenseByID(licenseID, ref applicationID, ref driverID,
                                                                                          ref issuedUsingLocalLicenseID, ref issueDate,
                                                                                          ref expirationDate, ref isActive, ref createdByUserID);

            if (isFound)
            {
                // find the base object of clsApplication
                clsApplication Application = clsApplication.FindApplication(applicationID);


                return new clsInternationalLicense(licenseID, applicationID, driverID, issuedUsingLocalLicenseID,
                                                                issueDate, expirationDate, isActive,Application.ApplicantPersonID,
                                                                Application.ApplicationDate, Application.ApplicationStatus, Application.LastStatusDate,
                                                                Application.PaidFees,Application.CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsLocaLicenseLicenseUsedByID(int LocalLicenseID, ref int InternaionalLicenseID)
        {
            return clsInternationalLicenseDataAccess.IsLocalLicenseUsedByLicenseID(LocalLicenseID, ref InternaionalLicenseID);
        }

        public static bool IsLocaLicenseLicenseUsedByID(int LocalLicenseID)
        {
            int defaultInternaionalLicenseID = 0;
            return clsInternationalLicenseDataAccess.IsLocalLicenseUsedByLicenseID(LocalLicenseID, ref defaultInternaionalLicenseID);
        }


        static public DataTable GetInternationalApplications()
        {
          return  clsInternationalLicenseDataAccess.GetInternationalApplications();
        }
         
        static public DataTable GetAllLicenseInternationalForPersonByPersonID(int PersonID)
        {
            return clsInternationalLicenseDataAccess.GetAllLicenseInternationalForPersonByPersonID(PersonID);
        }


        public static bool IsLicenseExist(int licenseID)
        {
            return clsInternationalLicenseDataAccess.IsInternationalLicenseExistByLicenseID(licenseID);
        }

        public bool Save()
        {
            //B first we call the save method in the base class

            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //case enMode.Update:

                    //return _UpdateInternationalLicense();

            }

            return false;
        }

    }




    
    /// FOR USER CONTROL
 public class clsShowInternationalLicenseBusinessLayer
        {
            public int InternationalLicenseID { get; private set; }
            public string FullName { get; private set; }
            public string Gender { get; private set; }
            public string IsActive { get; private set; }
            public int LicenseID { get; private set; }
            public string NationalNo { get; private set; }
            public DateTime IssueDate { get; private set; }
            public DateTime ExpirationDate { get; private set; }
            public DateTime DateOfBirth { get; private set; }
            public int DriverID { get; private set; }
            public int ApplicationID { get; private set; }

        public clsShowInternationalLicenseBusinessLayer() { }

            private clsShowInternationalLicenseBusinessLayer(int internationalLicenseID, string fullName, string gender, string isActive,
                                                         int licenseID, string nationalNo, DateTime issueDate, DateTime expirationDate,
                                                         DateTime dateOfBirth, int driverID, int ApplicationID)
            {
                InternationalLicenseID = internationalLicenseID;
                FullName = fullName;
                Gender = gender;
                IsActive = isActive;
                LicenseID = licenseID;
                NationalNo = nationalNo;
                IssueDate = issueDate;
                ExpirationDate = expirationDate;
                DateOfBirth = dateOfBirth;
                DriverID = driverID;
                this.ApplicationID = ApplicationID;
            }

            // Find License Details By ID
            public static clsShowInternationalLicenseBusinessLayer FindLicenseByID(int internationalLicenseID)
            {
                string fullName = "";
                string gender = "";
                string isActive = "";
                int licenseID = 0;
                string nationalNo = "";
                DateTime issueDate = DateTime.MinValue;
                DateTime expirationDate = DateTime.MinValue;
                DateTime dateOfBirth = DateTime.MinValue;
                int driverID = 0;
                int applicationID = 0;

                bool isFound = clsInternationalLicenseDataAccess.FindInternationalLicenseDetailsByID(
                    internationalLicenseID, ref fullName, ref gender, ref isActive, ref licenseID,
                    ref nationalNo, ref issueDate, ref expirationDate, ref dateOfBirth, ref driverID, ref applicationID);

                if (isFound)
                {
                    return new clsShowInternationalLicenseBusinessLayer(
                        internationalLicenseID, fullName, gender, isActive, licenseID, nationalNo,
                        issueDate, expirationDate, dateOfBirth, driverID, applicationID);
                }
                else
                {
                    return null;
                }
            }
        }



}






