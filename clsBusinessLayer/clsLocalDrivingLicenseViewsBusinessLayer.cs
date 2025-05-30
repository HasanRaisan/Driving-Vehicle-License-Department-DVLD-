using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsLocalDrivingLicenseViewsBusinessLayer
    {
       public int _LocalDrivingLicenseApplicationID { get; set; }
       public string _ClassName { get; set; }
       public string _NationalNo { get; set; }
       public string _FullName { get; set; }
       public DateTime _ApplicationDate { get; set; }
       public int _PassedTestCount { get; set; }
       public string _Status {  get; set; }
       public int BaseApplicationID { get; set; }


        public clsLocalDrivingLicenseViewsBusinessLayer () { }

        clsLocalDrivingLicenseViewsBusinessLayer(int LocalDrivingLicenseApplicationID, string ClassName, string NationalNo,  string FullName,
                                              DateTime ApplicationDate, int PassedTestCount, string Status, int BaseApplicationID)
        {
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this._ClassName = ClassName;
            this._NationalNo = NationalNo;
            this._FullName = FullName;
            this._ApplicationDate = ApplicationDate;
            this._PassedTestCount = PassedTestCount;
            this._Status = Status;
            this.BaseApplicationID = BaseApplicationID;
           
        }
        

        static public  clsLocalDrivingLicenseViewsBusinessLayer FindLDLAppView(int LocalDrivingLicenseApplicationID)
        {
               string ClassName  = string.Empty;
               string NationalNo  = string.Empty;
               string FullName    = string.Empty;
               DateTime ApplicationDate = DateTime.MinValue;
               int PassedTestCount = byte.MinValue;
               string Status = string.Empty;
               int BaseApplicationID = 0;


            if (clsViewsDataAccess.FindLDLApp_View(LocalDrivingLicenseApplicationID, ref ClassName, ref NationalNo, ref FullName, ref ApplicationDate, ref PassedTestCount, ref Status, ref BaseApplicationID))
            {
                return new clsLocalDrivingLicenseViewsBusinessLayer(LocalDrivingLicenseApplicationID,  ClassName,  NationalNo,  FullName,  ApplicationDate,  PassedTestCount,  Status, BaseApplicationID);
            }
            else return null;
        }

    }







    public class clsLicenseDetailsViewBusinessLayer
    {
        public int _LicenseID { get; set; }
        public string _ClassName { get; set; }
        public string _FullName { get; set; }
        public string _NationalNo { get; set; }
        public string _Gender { get; set; }
        public DateTime _IssueDate { get; set; }
        public string _Notes { get; set; }
        public string _IsActive { get; set; }
        public DateTime _DateOfBirth { get; set; }
        public DateTime _ExpirationDate { get; set; }
        public string _IsDetained { get; set; }
        public int _DriverID { get; set; }
        public clsPerson PersonInfo { get; set; }

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public enIssueReason _IssueReason { get; set; }
        public string IssueReasonText
        {
            get { return GetIssueReasonText(_IssueReason); }
        }

        public clsLicenseDetailsViewBusinessLayer() { }

        private clsLicenseDetailsViewBusinessLayer(int LicenseID, string ClassName, string FullName, string NationalNo,
                                               string Gender, DateTime IssueDate, enIssueReason IssueReason, string Notes,
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
            this.PersonInfo = clsPerson.FindPerson(NationalNo);
        }

        static public clsLicenseDetailsViewBusinessLayer FindLicenseDetailsByLicenseID(int LicenseID)
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

            if (clsViewsDataAccess.FindLicenseDetailsViewByLicenseID(LicenseID, ref ClassName, ref FullName, ref NationalNo, ref Gender,
                                                                      ref IssueDate, ref IssueReason, ref Notes, ref IsActive,
                                                                      ref DateOfBirth, ref ExpirationDate, ref IsDetained, ref DriverID))
            {
                return new clsLicenseDetailsViewBusinessLayer(LicenseID, ClassName, FullName, NationalNo, Gender, IssueDate,
                                                           (enIssueReason)IssueReason, Notes, IsActive, DateOfBirth, ExpirationDate,
                                                          IsDetained, DriverID);
            }
            else
            {
                return null;
            }
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
    }






}
