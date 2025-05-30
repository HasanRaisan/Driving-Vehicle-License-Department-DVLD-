using DataAccessLayer;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;


namespace BusinessLayer
{
    public class clsApplications
    {
        //_ApplicantPersonID
        //_ApplicationDate  
        //_ApplicationTypeID
        //_ApplicationStatus
        //LastStatusDate   
        //_PaidFees         
        //_CreatedByUserID  
        
        public int _ApplicationID { get; set; }
        public int _ApplicantPersonID { get; set; }
        public int _ApplicationTypeID { get; set; }
        public int _CreatedByUserID { get; set; }
        public int _LicenseClassID { get; set; }
        public byte _ApplicationStatus { get;  set; }
        public decimal _PaidFees { get; set; }
        public DateTime _ApplicationDate;
        public DateTime _LastStatusDate;


        public clsApplications() { }
        
        private clsApplications(int ApplicationID, int ApplicantPersonID, int ApplicationTypeID , int LicenseClassID, int CreatedByUser,
                                              decimal PaidFees, byte ApplicationStatus, DateTime ApplicationDate, DateTime LastStatusDate)
        {
           this. _ApplicationID = ApplicationID;
            this._ApplicantPersonID = ApplicantPersonID;
            this._ApplicationTypeID = ApplicationTypeID;
            this._LicenseClassID = LicenseClassID;
            this._CreatedByUserID = CreatedByUser;
            this._PaidFees = PaidFees;
            this._ApplicationStatus = ApplicationStatus;
            this._ApplicationDate = ApplicationDate;
            this._LastStatusDate = LastStatusDate;


        }
        private bool _AddNewApplication()
        {
            this._ApplicationID = clsApplicationsDataAccess.AddNewApplication(this._ApplicantPersonID, this._ApplicationTypeID,
                                                          this._PaidFees, this._CreatedByUserID, this._LicenseClassID);
            return (this._ApplicationID != 0);
        }

        public static clsApplications FindApplication(int ApplicationID)
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
                return new clsApplications(ApplicationID,ApplicantPersonID,ApplicationTypeID, LicenseClassID, CreatedByUser, PaidFees,  ApplicationStatus, ApplicationDate, LastStatusDate);
            }
            else return null;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationIDForLicenseClass(PersonID, ApplicationTypeID, LicenseClassID);
        }


        static public bool UpdateApplicationStatus(int ApplicationID, byte ApplicationStatus)
        {

            return clsApplicationsDataAccess.UpdateStatus(ApplicationID, ApplicationStatus);
        }

        static public DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.GetAllApplications();
        }

        public bool DeleteApplication()
        {
            return clsApplicationsDataAccess.DeleteApplication(this._ApplicationID);
        }

        static public bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsDataAccess.DeleteApplication(ApplicationID);
        }

        private bool _DeleteApplication()
        {
            return clsApplicationsDataAccess.DeleteApplication(this._ApplicationID);

        }

        public bool Save()
        {
            return _AddNewApplication();
        }
    
    }
}
