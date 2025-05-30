

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsLicenses
    {


        public int  _LicenseID  { get; set; }
        public int  _ApplicationID { get; set; }
        public int  _DriverID { get; set; }
        public int  _LicenseClassID {  get; set; }
        public DateTime  _IssueDate { get; private set; }
        public DateTime  _ExpirationDate { get; set; }
        public string  _Notes { get; set; }
        public decimal  _PaidFees { get; set; }
        public bool  _IsActive { get; set; }
        public int  _CreatedByUserID { get; set; }

        enum enMode { AddNew = 1, UpdateNew = 2 }   
        enMode Mode { get; set; }


        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public enIssueReason IssueReason { get; set; }  
        public string IssueReasonText
        {
            get { return GetIssueReasonText(IssueReason); }
        }

        public clsDrivers DriverInfo { get; set; }
        public clsLicenseClasses LicenseClassesInfo { get; set; }
        public clsDetainLicense DetainedInfo { get; set; }


        public clsLicenses()
        {
            this._LicenseID = -1;
            this._DriverID = -1;
            this._LicenseClassID = -1;
            this._IsActive = false;
            this._CreatedByUserID = -1;
            this._ApplicationID = -1;
            this._PaidFees = -1;



            this.Mode = enMode.AddNew;
        }
        private clsLicenses(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate,
                                      DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this._LicenseID = LicenseID;
            this._ApplicationID = ApplicationID;
            this._DriverID = DriverID;
            this._LicenseClassID = LicenseClassID;
            this._IssueDate = IssueDate;
            this._ExpirationDate = ExpirationDate;
            this._Notes = Notes;
            this._PaidFees = PaidFees;
            this._IsActive = IsActive;
            this.IssueReason = (enIssueReason)IssueReason;
            this._CreatedByUserID = CreatedByUserID;
            this.Mode = enMode.UpdateNew;


            this.DriverInfo = clsDrivers.FindDriver(DriverID);
            this.LicenseClassesInfo = clsLicenseClasses.FindLicenseClass(LicenseClassID);
            this.DetainedInfo = clsDetainLicense.FindByLicenseID(LicenseID);
        }

        static public clsLicenses FindLicense(int LicenseID)
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

                return new clsLicenses(LicenseID, _ApplicationID, _DriverID, _LicenseClass, _IssueDate, _ExpirationDate,
                    _Notes, _PaidFees, _IsActive, _IssueReason, _CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewLicense()
        {
            this._LicenseID = clsLicenseDataAccess.AddNewLicense(this._ApplicationID, this._DriverID, this._LicenseClassID,
                this._ExpirationDate, this._Notes, this._PaidFees, this._IsActive, (byte) this.IssueReason, this._CreatedByUserID);
            return (this._LicenseID != -1);
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
           return clsLicenseDataAccess.UpdateLicenseActivationStatus(this._LicenseID,this._IsActive);
        }

       static public bool isLicenseExistByPersonID( int PersonID , int LicenseClassID)
        {
            return clsLicenseDataAccess.GetLicenseIDByPersonID(PersonID, LicenseClassID) != -1;
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



        ////////
        ////////
        ////////
        ///  
        //////  Compsetion ///////

        

        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainLicense detainedLicense = new clsDetainLicense();
            detainedLicense.LicenseID = this._LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToDecimal(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {

                return -1;
            }

            return detainedLicense.DetainID;

        }

        //public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        //{

        //    //First Create Applicaiton 
        //    clsApplications Application = new clsApplications();

        //    Application._ApplicantPersonID = this.DriverInfo._PersonID;
        //    Application._ApplicationDate = DateTime.Now;
        //    Application._ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
        //    Application._ApplicationStatus = clsApplications.enApplicationStatus.Completed;
        //    Application._LastStatusDate = DateTime.Now;
        //    Application._PaidFees = clsApplicationTypes.Find((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense).Fees;
        //    Application._CreatedByUserID = ReleasedByUserID;

        //    if (!Application.Save())
        //    {
        //        ApplicationID = -1;
        //        return false;
        //    }

        //    ApplicationID = Application._ApplicationID;


        //    return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application._ApplicationID);

        //}


        /*

        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {

            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassIfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassIfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }

        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {


            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find(Application.ApplicationTypeID).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
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

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }

        */
    }
}
