using DataAccessLayer;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;


namespace BusinessLayer
{
    public class clsApplication
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
        public enApplicationStatus ApplicationStatus { set; get; }
        public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public int ApplicationTypeID { get; set; }
        public int CreatedByUserID { get; set; }
        public int LicenseClassID { get; set; }
        public decimal PaidFees { get; set; }
        public DateTime ApplicationDate;
        public DateTime LastStatusDate;
        public clsUser CreatedByUserInfo;
        public clsApplicationType ApplicationTypeInfo;



        public clsApplication() {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        
        private clsApplication(int ApplicationID, int ApplicantPersonID, int ApplicationTypeID , int LicenseClassID, int CreatedByUser,
                                              decimal PaidFees, enApplicationStatus ApplicationStatus, DateTime ApplicationDate, DateTime LastStatusDate)
        {
           this. ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.LicenseClassID = LicenseClassID;
            this.CreatedByUserID = CreatedByUser;
            this.PaidFees = PaidFees;
            this.ApplicationStatus = ApplicationStatus;
            this.ApplicationDate = ApplicationDate;
            this.LastStatusDate = LastStatusDate;
            this.ApplicationTypeInfo = clsApplicationType.FindApplication(ApplicationTypeID);
            this.CreatedByUserInfo = clsUser.FindUser(CreatedByUserID);
            Mode = enMode.Update;
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(this.ApplicantPersonID, this.ApplicationTypeID,
                                                          this.PaidFees, this.CreatedByUserID, this.LicenseClassID);
            return (this.ApplicationID != 0);
        }

        public static clsApplication FindApplication(int ApplicationID)
        {
            // the other Find function handled with [is exist function]

            int ApplicantPersonID = -1;
            byte ApplicationStatus = 0;
            int ApplicationTypeID = -1;
            decimal PaidFees = 0;
            int CreatedByUser = -1;
            int LicenseClassID = int.MinValue;
            DateTime ApplicationDate = new DateTime();
            DateTime LastStatusDate = new DateTime();

            if (clsApplicationsDataAccess.FindApplication( ApplicationID, ref  ApplicantPersonID, ref ApplicationTypeID, ref LicenseClassID, ref CreatedByUser,
                                                          ref PaidFees, ref ApplicationStatus, ref  ApplicationDate, ref  LastStatusDate))
            {
                return new clsApplication(ApplicationID,ApplicantPersonID,ApplicationTypeID, LicenseClassID, CreatedByUser, PaidFees, (enApplicationStatus)ApplicationStatus, ApplicationDate, LastStatusDate);
            }
            else return null;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationIDForLicenseClass(PersonID, (int) ApplicationTypeID, LicenseClassID);
        }


        static public bool UpdateApplicationStatus(int ApplicationID, byte ApplicationStatus)
        {

            return clsApplicationsDataAccess.UpdateStatus(ApplicationID, ApplicationStatus);
        }

        public bool Cancel()

        {
            return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID, 2);
        }

        public bool SetComplete()

        {
            return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID, 3);
        }

        public bool DeleteApplication()
        {
            return clsApplicationsDataAccess.DeleteApplication(this.ApplicationID);
        }


        static public DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.GetAllApplications();
        }

        static public bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsDataAccess.DeleteApplication(ApplicationID);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:

                    if (_AddNewApplication())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                //case enMode.Update: return ();

                default: return false;

            }
        }

    }
}
