using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsViewsBusinessLayer
    {
       public int _LocalDrivingLicenseApplicationID { get; set; }
       public string _ClassName { get; set; }
       public string _NationalNo { get; set; }
       public string _FullName { get; set; }
       public DateTime _ApplicationDate { get; set; }
       public int _PassedTestCount { get; set; }
       public string _Status {  get; set; }


        public clsViewsBusinessLayer () { }

        clsViewsBusinessLayer(int LocalDrivingLicenseApplicationID, string ClassName, string NationalNo,  string FullName,
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
        

        static public  clsViewsBusinessLayer FindLDLAppView(int LocalDrivingLicenseApplicationID)
        {
               string ClassName  = string.Empty;
               string NationalNo  = string.Empty;
               string FullName    = string.Empty;
               DateTime ApplicationDate = DateTime.MinValue;
               int PassedTestCount = byte.MinValue;
               string Status = string.Empty;


            if (clsViewsDataAccess.FindLDLApp_View(LocalDrivingLicenseApplicationID, ref ClassName, ref NationalNo, ref FullName, ref ApplicationDate, ref PassedTestCount, ref Status))
            {
                return new clsViewsBusinessLayer(LocalDrivingLicenseApplicationID,  ClassName,  NationalNo,  FullName,  ApplicationDate,  PassedTestCount,  Status);
            }
            else return null;
        }

    }
}
