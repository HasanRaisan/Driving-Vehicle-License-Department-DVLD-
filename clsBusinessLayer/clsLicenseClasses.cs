using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.clsLicenseClassDataAccess;

namespace BusinessLayer
{
    public class clsLicenseClasses
    {


        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }

        public clsLicenseClasses() { }

        private clsLicenseClasses(int licenseClassID, string className, string classDescription,
                                               byte minimumAllowedAge, byte defaultValidityLength, decimal classFees)
        {
            this.LicenseClassID = licenseClassID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = classFees;
        }

        public static clsLicenseClasses FindLicenseClass(int LicenseClassID)
        {
            string _ClassName = null;
            string _ClassDescription = null;
            byte _MinimumAllowedAge = 0;
            byte _DefaultValidityLength = 0;
            decimal _ClassFees = 0;

            if (clsLicenseClassDataAccess.FindLicenseClassByID(LicenseClassID, ref _ClassName, ref _ClassDescription,
                                                                  ref _MinimumAllowedAge, ref _DefaultValidityLength, ref _ClassFees))
            {
                return new clsLicenseClasses(LicenseClassID, _ClassName, _ClassDescription,
                                                          _MinimumAllowedAge, _DefaultValidityLength, _ClassFees);
            }
            else
            {
                return null;
            }
        }


        public static clsLicenseClasses FindLicenseClass(string ClassNameToSearch)
        {
            int _LicenseClassID = 0;
            string _ClassName = null;
            string _ClassDescription = null;
            byte _MinimumAllowedAge = 0;
            byte _DefaultValidityLength = 0;
            decimal _ClassFees = 0;

            if (clsLicenseClassDataAccess.FindLicenseClassByName(ClassNameToSearch, ref _LicenseClassID, ref _ClassName,
                                                                  ref _ClassDescription, ref _MinimumAllowedAge,
                                                                  ref _DefaultValidityLength, ref _ClassFees))
            {
                return new clsLicenseClasses(_LicenseClassID, _ClassName, _ClassDescription,
                                             _MinimumAllowedAge, _DefaultValidityLength, _ClassFees);
            }
            else
            {
                return null;
            }
        }


        static public DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassDataAccess.GetAllLicenseClasses();
        }


    }
}
