using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsDetainLicense
    {        
        private enMode _Mode { get; set; }
        private enum enMode { AddNew = 0, Update = 1 };


        public int LicenseID { get; set; }
        public int DetainID { get; private set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; private set; }

        public clsUser CreatedByUserInfo { get; set; }



        // Release
        private DateTime? _ReleaseDate;
        private int? _ReleasedByUserID;
        private int? _ReleaseApplicationID;

        public clsUser ReleasedByUserInfo { get; set; }


        //Cannot modify release details for a new object.
        public int? ReleasedByUserID
        {
            get
            {
                return _ReleasedByUserID;
            }
            set
            {
                if (this._Mode == enMode.Update)
                    _ReleasedByUserID = value;
                else
                    throw new InvalidOperationException("Cannot modify ReleasedByUserID for a new object.");
            }
        }
        public int? ReleaseApplicationID
        {
            get
            {
                return _ReleaseApplicationID;
            }
            set
            {
                if (this._Mode == enMode.Update)
                    _ReleaseApplicationID = value;
                else
                    throw new InvalidOperationException("Cannot modify ReleaseApplicationID for a new object.");
            }
        }



        public clsDetainLicense()
        {
            this.LicenseID = -1;
            this.DetainID = -1;
            this.DetainDate = DateTime.MinValue;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;

            this._Mode = enMode.AddNew;
        }

        private clsDetainLicense(int LicenseID, int DetainID, DateTime DetainDate, decimal FineFees, int CreatedByUserID,
                                               bool IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            this.LicenseID = LicenseID;
            this.DetainID = DetainID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this._ReleaseDate = ReleaseDate;
            this._ReleasedByUserID = ReleasedByUserID;
            this._ReleaseApplicationID = ReleaseApplicationID;
            this.CreatedByUserInfo = clsUser.FindUser(CreatedByUserID);
            this.ReleasedByUserInfo = clsUser.FindUser(ReleasedByUserID.HasValue ? ReleasedByUserID.Value : -1);
            this._Mode = enMode.Update;
        }

        private bool _DetainLicense()
        {
            this.DetainID = clsDetainLicenseDataAccess.DetaineLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            return (this.DetainID != -1);
        }

        private bool _ReleaseDetainedLicense()
        {
            this._ReleaseDate = DateTime.Now;
            if (_ReleasedByUserID == null || _ReleaseApplicationID == null)
                throw new InvalidOperationException("Release details must be set before releasing the license.");

            return clsDetainLicenseDataAccess.ReleaseDetainedLicense(this.LicenseID, this._ReleaseDate.Value, this._ReleasedByUserID.Value, this._ReleaseApplicationID.Value);
        }


        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    this._Mode = enMode.Update;
                    return _DetainLicense();

                case enMode.Update:
                    return _ReleaseDetainedLicense();

                default:
                    return false;
            }
        }

        static public bool IsLicenseDetainedByLicenseID(int LicenseID)
        {
            return clsDetainLicenseDataAccess.IsLicenseDetainedByLicenseID(LicenseID);
        }

        public static clsDetainLicense FindByLicenseID(int licenseID)
        {
            int detainID = 0;
            DateTime detainDate = DateTime.MinValue;
            decimal fineFees = 0;
            int createdByUserID = 0;
            bool isReleased = false;
            DateTime? releaseDate = null;
            int? releasedByUserID = null;
            int? releaseApplicationID = null;

            bool isFound = clsDetainLicenseDataAccess.FindDetainedLicenseByLicenseID(licenseID, ref detainID, ref detainDate, ref fineFees,
                                                                             ref createdByUserID, ref isReleased, ref releaseDate,
                                                                             ref releasedByUserID, ref releaseApplicationID);

            if (isFound)
            {
                return new clsDetainLicense(licenseID, detainID, detainDate, fineFees, createdByUserID, isReleased,
                                                        releaseDate, releasedByUserID, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsDetainReleaseByDetainID(int DetainID)
        {
            return clsDetainLicenseDataAccess.IsThisDetainReleaseByDetainID(DetainID);
        }

        public static DataTable GetAllDetainedLicense()
        {
          return  clsDetainLicenseDataAccess.GetAllDetains();
        }
    }

}
