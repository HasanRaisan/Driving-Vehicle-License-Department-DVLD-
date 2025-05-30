using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsLocalDrivingLicenseViews
    {
       public int _LocalDrivingLicenseApplicationID { get; set; }
       public string _ClassName { get; set; }
       public string _NationalNo { get; set; }
       public string _FullName { get; set; }
       public DateTime _ApplicationDate { get; set; }
       public int _PassedTestCount { get; set; }
       public string _Status {  get; set; }


        public clsLocalDrivingLicenseViews () { }

        clsLocalDrivingLicenseViews(int LocalDrivingLicenseApplicationID, string ClassName, string NationalNo,  string FullName,
                                              DateTime ApplicationDate, int PassedTestCount, string Status)
        {
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this._ClassName = ClassName;
            this._NationalNo = NationalNo;
            this._FullName = FullName;
            this._ApplicationDate = ApplicationDate;
            this._PassedTestCount = PassedTestCount;
            this._Status = Status;
            
           
        }
        

        static public  clsLocalDrivingLicenseViews FindLDLAppView(int LocalDrivingLicenseApplicationID)
        {
               string ClassName  = string.Empty;
               string NationalNo  = string.Empty;
               string FullName    = string.Empty;
               DateTime ApplicationDate = DateTime.MinValue;
               int PassedTestCount = byte.MinValue;
               string Status = string.Empty;


            if (clsViews.FindLDLApp_View(LocalDrivingLicenseApplicationID, ref ClassName, ref NationalNo, ref FullName, ref ApplicationDate, ref PassedTestCount, ref Status))
            {
                return new clsLocalDrivingLicenseViews(LocalDrivingLicenseApplicationID,  ClassName,  NationalNo,  FullName,  ApplicationDate,  PassedTestCount,  Status);
            }
            else return null;
        }

    }







    public class clsLicenseWithDetailsViewBusinessLayer
    {
        public int _LicenseID { get; set; }
        public string _ClassName { get; set; }
        public string _FullName { get; set; }
        public string _NationalNo { get; set; }
        public string _Gender { get; set; }
        public DateTime _IssueDate { get; set; }
        public byte _IssueReason { get; set; }
        public string _Notes { get; set; }
        public string _IsActive { get; set; }
        public DateTime _DateOfBirth { get; set; }
        public DateTime _ExpirationDate { get; set; }
        public string _IsDetained { get; set; }
        public int _DriverID { get; set; }


        public clsLicenseWithDetailsViewBusinessLayer() { }

        private clsLicenseWithDetailsViewBusinessLayer(int LicenseID, string ClassName, string FullName, string NationalNo,
                                               string Gender, DateTime IssueDate, byte IssueReason, string Notes,
                                               string IsActive, DateTime DateOfBirth, DateTime ExpirationDate,
                                               string IsDetained, int DriverID)
        {
            this._LicenseID = LicenseID;
            this._ClassName = ClassName;
            this._FullName = FullName;
            this._NationalNo = NationalNo;
            this._Gender = Gender;
            this._IssueDate = IssueDate;
            this._IssueReason = IssueReason;
            this._Notes = Notes;
            this._IsActive = IsActive;
            this._DateOfBirth = DateOfBirth;
            this._ExpirationDate = ExpirationDate;
            this._IsDetained = IsDetained;
            this._DriverID = DriverID;
        }

        static public clsLicenseWithDetailsViewBusinessLayer FindLicenseDetailsByLicenseID(int LicenseID)
        {
            string ClassName = string.Empty;
            string FullName = string.Empty;
            string NationalNo = string.Empty;
            string Gender = string.Empty;
            DateTime IssueDate = DateTime.MinValue;
            byte IssueReason = 0;
            string Notes = string.Empty;
            string IsActive = string.Empty;
            DateTime DateOfBirth = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string IsDetained = string.Empty;
            int DriverID = 0;

            if (clsViews.FindLicenseDetailsViewByLicenseID(LicenseID, ref ClassName, ref FullName, ref NationalNo, ref Gender,
                                                                      ref IssueDate, ref IssueReason, ref Notes, ref IsActive,
                                                                      ref DateOfBirth, ref ExpirationDate, ref IsDetained, ref DriverID))
            {
                return new clsLicenseWithDetailsViewBusinessLayer(LicenseID, ClassName, FullName, NationalNo, Gender, IssueDate,
                                                          IssueReason, Notes, IsActive, DateOfBirth, ExpirationDate,
                                                          IsDetained, DriverID);
            }
            else
            {
                return null;
            }
        }
    }




    public class clsPeopleWithTheirLicensClassessIDBusinessLayer
    {
        public int PersonID { get; set; }
        public int LicenseClassID { get; set; }

        public clsPeopleWithTheirLicensClassessIDBusinessLayer() { }

        public clsPeopleWithTheirLicensClassessIDBusinessLayer(int personID, int licenseClassID)
        {
            this.PersonID = personID;
            this.LicenseClassID = licenseClassID;
        }


        public static bool DoesPersonHaveThisLicense(int personID, int licenseClassID)
        {
            return clsViews.DosePersonHaveThisLicense(personID, licenseClassID);
        }
    }


}
