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
    public class clsInternationalLicenses
    {
        public int InternationalLicenseID { get; private set; }
        public int ApplicationID { get;  set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }



        public clsInternationalLicenses() { }   
        private clsInternationalLicenses(int licenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID,
                                                    DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            InternationalLicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;
        }

        private bool _AddNewLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseDataAccess.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                                                                                this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);

        }

        public static clsInternationalLicenses FindLicenseByID(int licenseID)
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
                return new clsInternationalLicenses(licenseID, applicationID, driverID, issuedUsingLocalLicenseID,
                                                                issueDate, expirationDate, isActive, createdByUserID);
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
            return this._AddNewLicense();
        }

    }




    
    /// FORM USER CONTROL
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






