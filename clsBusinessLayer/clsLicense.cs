

using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
    public class clsLicense
    {


        public int  LicenseID  { get; set; }
        public int  ApplicationID { get; set; }
        public int  DriverID { get; set; }
        public int  LicenseClassID {  get; set; }
        public DateTime  IssueDate { get; private set; }
        public DateTime  ExpirationDate { get; set; }
        public string  Notes { get; set; }
        public decimal  PaidFees { get; set; }
        public bool  IsActive { get; set; }
        public int  CreatedByUserID { get; set; }

        enum enMode { AddNew = 1, UpdateNew = 2 }   
        enMode Mode { get; set; }


        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public enIssueReason IssueReason { get; set; }  
        public string IssueReasonText
        {
            get { return GetIssueReasonText(IssueReason); }
        }

        public bool IsDetained
        {
            get { return clsDetainLicense.IsLicenseDetainedByLicenseID(this.LicenseID); }
        }

        public clsDriver DriverInfo { get; set; }
        public clsLicenseClasses LicenseClasseInfo { get; set; }
        public clsDetainLicense DetainedInfo { get; set; }



        public clsLicense()
        {
            this.LicenseID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            this.ApplicationID = -1;
            this.PaidFees = -1;
            this.IssueReason = enIssueReason.FirstTime;
            this.Mode = enMode.AddNew;
        }
        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate,
                                      DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = (enIssueReason)IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.Mode = enMode.UpdateNew;


            this.DriverInfo = clsDriver.FindDriver(DriverID);
            this.LicenseClasseInfo = clsLicenseClasses.FindLicenseClass(LicenseClassID);
            this.DetainedInfo = clsDetainLicense.FindByLicenseID(LicenseID);
        }

        static public clsLicense FindLicense(int LicenseID)
        {
            if (clsLicenseDataAccess.IsLicenseExistByLicenseID(LicenseID))
            {
                int _ApplicationID = -1;
                int _DriverID = -1;
                int _LicenseClass = -1;
                DateTime _IssueDate = DateTime.Now;
                DateTime _ExpirationDate = DateTime.Now;
                string _Notes = null;
                decimal _PaidFees = 0;
                bool _IsActive = false;
                byte _IssueReason = 0;
                int _CreatedByUserID = -1;

                clsLicenseDataAccess.FindLicenseByID(LicenseID, ref _ApplicationID, ref _DriverID, ref _LicenseClass, ref _IssueDate,
                    ref _ExpirationDate, ref _Notes, ref _PaidFees, ref _IsActive, ref _IssueReason, ref _CreatedByUserID);

                return new clsLicense(LicenseID, _ApplicationID, _DriverID, _LicenseClass, _IssueDate, _ExpirationDate,
                    _Notes, _PaidFees, _IsActive, _IssueReason, _CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID,
                this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte) this.IssueReason, this.CreatedByUserID);
            return (this.LicenseID != -1);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (this._AddNewLicense())
                {
                    Mode = enMode.UpdateNew;
                    return true;
                }
            }
            return false;
        }

        static public bool IsLicenseExist(int LicenseID)
        {
            return clsLicenseDataAccess.IsLicenseExistByLicenseID(LicenseID);
        }

        static public int GetLicenseIDByApplicationID(int AppID)
        {
            return clsLicenseDataAccess.GetLicenseIDByApplicationID(AppID);
        }
        static public int GetPersonIDByLicenseID(int LicenseID)
        {
            return clsLicenseDataAccess.GetPerosnIDByLicenseID(LicenseID);
        }


        static public DataTable GetAllLicenseForPersonByPersonID(int  PersonID)
        {
            return clsLicenseDataAccess.GetAllPersonLicenses(PersonID);
        }

        public static bool IsLicenseExpired(int licenseID)
        {
            return clsLicenseDataAccess.IsLicenseExpired(licenseID);
        }

        public  bool UpdateLicenseActivationStatus()
        {
           return clsLicenseDataAccess.UpdateLicenseActivationStatus(this.LicenseID,this.IsActive);
        }

       static public bool isLicenseExistByPersonIDAndLicenseClassID( int PersonID , int LicenseClassID)
        {
            return clsLicenseDataAccess.GetLicenseIDByPersonIDAndLicenceClassID(PersonID, LicenseClassID) != -1;
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }



        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseDataAccess.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }


        ////////
        ////////
        ////////
        ///  
        //////  Compsetion ///////



        public int Detain(decimal FineFees, int CreatedByUserID)
        {
            //First Create Applicaiton 
            clsDetainLicense detainedLicense = new clsDetainLicense();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = FineFees;
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {

                return -1;
            }

            return detainedLicense.DetainID;

        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {

            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.FindApplication((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;
            Application.LicenseClassID = this.LicenseClassID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.ApplicationID;

            DetainedInfo.ReleasedByUserID = ReleasedByUserID;
            DetainedInfo.ReleaseApplicationID = ApplicationID;
            return this.DetainedInfo.Save();

        }

        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {

            //First WE Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.FindApplication((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;
            Application.LicenseClassID = this.LicenseClassID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClasseInfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClasseInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            //we must to deactivate the old License.
            clsLicenseDataAccess.UpdateLicenseActivationStatus(this.LicenseID, false);

            return NewLicense;
        }



        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {


            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.FindApplication(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;
            Application.LicenseClassID = this.LicenseClassID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;




            if (!NewLicense.Save())
            {
                return null;
            }

            //we must to deactivate the old License.
            clsLicenseDataAccess.UpdateLicenseActivationStatus(this.LicenseID, false);

            return NewLicense;
        }

     
    }
}
