using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{

    /*
    
    public class clsDetainLicense
    {
        public int LicenseID { get; set; }
        public int DetainID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; private set; }
        public DateTime? _ReleaseDate { get; private set; }
        public int? _ReleasedByUserID { get; private set; }
        public int? _ReleaseApplicationID { get; private set; }



        private bool _isRetrievedFromDatabase;

        private enMode _Status { get; set; }

        private enum enMode { AddNew = 0, Update = 1 };


        public int? ReleasedByUserID
        {
            get
            {
                return _ReleasedByUserID;
            }
            set
            {
                if (_isRetrievedFromDatabase)
                    _ReleasedByUserID = value;
                else
                    throw new InvalidOperationException("Cannot modify ReleasedByUserID for a new object.");
            }
        }


        public clsDetainLicense()
        {
            this._Status = enMode.AddNew;
        }

        private clsDetainLicense(int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID,
                                              bool IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this._ReleaseDate = ReleaseDate;
            this._ReleasedByUserID = ReleasedByUserID;
            this._ReleaseApplicationID = ReleaseApplicationID;
            this._Status = enMode.Update;
        }

        private bool _DetainLicense()
        {
            this.DetainID = clsDetainLicenseDataAccess.DetaineLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return (this.DetainID != -1);

        }

        private bool _ReleaseDetainedLicense()
        {
            return clsDetainLicenseDataAccess.ReleaseDetainedLicense(this.LicenseID, this._ReleaseDate.Value, this._ReleasedByUserID.Value, this._ReleaseApplicationID.Value);
        }

        public bool SetReleaseDetails(DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            if (!_isRetrievedFromDatabase)
                throw new InvalidOperationException("Cannot modify release details for a new object.");

            this._ReleaseDate = releaseDate;
            this._ReleasedByUserID = releasedByUserID;
            this._ReleaseApplicationID = releaseApplicationID;

            return true;
        }

        public bool Save()
        {
            switch (this._Status)
            {
                case enMode.AddNew:
                    this._Status = enMode.Update;
                    return _DetainLicense();

                case enMode.Update:
                 return _ReleaseDetainedLicense();

                default:
                    return false;
            }
        }

  
    }


    */






    public class clsDetainLicense
    {
        public int LicenseID { get; set; }
        public int DetainID { get; private set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; private set; }



        // Release
        private DateTime? _ReleaseDate;
        private int? _ReleasedByUserID;
        private int? _ReleaseApplicationID;

        private bool _isRetrievedFromDatabase;
        private Status _Status { get; set; }
        private enum Status { AddNew = 0, Update = 1 };


        public DateTime? ReleaseDate
        {
            get
            {
                return _ReleaseDate;
            }
            set
            {
                if (_isRetrievedFromDatabase)
                    _ReleaseDate = value;
                else
                    throw new InvalidOperationException("Cannot modify ReleaseDate for a new object.");
            }
        }

        public int? ReleasedByUserID
        {
            get
            {
                return _ReleasedByUserID;
            }
            set
            {
                if (_isRetrievedFromDatabase)
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
                if (_isRetrievedFromDatabase)
                    _ReleaseApplicationID = value;
                else
                    throw new InvalidOperationException("Cannot modify ReleaseApplicationID for a new object.");
            }
        }

        //public bool SetReleaseDetails(DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        //{
        //    if (!_isRetrievedFromDatabase)
        //        throw new InvalidOperationException("Cannot modify release details for a new object.");

        //    this._ReleaseDate = releaseDate;
        //    this._ReleasedByUserID = releasedByUserID;
        //    this._ReleaseApplicationID = releaseApplicationID;

        //    return true;
        //}




        public clsDetainLicense()
        {
            this._Status = Status.AddNew;
            this._isRetrievedFromDatabase = false;
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
            this._Status = Status.Update;
            this._isRetrievedFromDatabase = true;
        }

        private bool _DetainLicense()
        {
            this.DetainID = clsDetainLicenseDataAccess.DetaineLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            return (this.DetainID != -1);
        }

        private bool _ReleaseDetainedLicense()
        {
            if (_ReleaseDate == null || _ReleasedByUserID == null || _ReleaseApplicationID == null)
                throw new InvalidOperationException("Release details must be set before releasing the license.");

            return clsDetainLicenseDataAccess.ReleaseDetainedLicense(this.LicenseID, this._ReleaseDate.Value, this._ReleasedByUserID.Value, this._ReleaseApplicationID.Value);
        }


        public bool Save()
        {
            switch (this._Status)
            {
                case Status.AddNew:
                    this._Status = Status.Update;
                    return _DetainLicense();

                case Status.Update:
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

        public static DataTable GetAllDetains()
        {
          return  clsDetainLicenseDataAccess.GetAllDetains();
        }
    }

}
