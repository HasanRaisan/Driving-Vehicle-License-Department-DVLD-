using ConnectionDataBaseLincense;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//using DataAccessLayer;

namespace BusinessLayer
{
    public class clsTestType
    {

        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode { get; set; } = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public clsTestType.enTestType ID { get; set; } = 0; // This is the primary key for the TestTypes table

        public decimal TestTypeFees { get; set; }
        public int TestTypesID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }



        static public DataTable GetAllTestType()
        {
            return clsTestTypeDataAccess.GetAllTestType();
        }


        public clsTestType()
        {
            this._Mode = enMode.AddNew;
            this.TestTypesID = -1;
            this.TestTypeTitle = "";
            this.ID = enTestType.VisionTest; // Default to VisionTest
        }


        clsTestType(enTestType TestTypesID, decimal TestTypeFees, string TestTypeTitle, string TestTypeDescription)
        {
            this.ID = TestTypesID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeFees = TestTypeFees;
            this.TestTypeDescription = TestTypeDescription;
        }

        static public clsTestType FindTestType(enTestType TestTypeID)
        {
            string TestTypeTitle = null;
            string TestTypeDescription = null;
            decimal TestTypeFees = 0;

            if (clsTestTypeDataAccess.FindTestType((int)TestTypeID, ref TestTypeFees, ref TestTypeTitle, ref TestTypeDescription))
            {
                return new clsTestType((enTestType)TestTypeID, TestTypeFees, TestTypeTitle, TestTypeDescription);
            }
            else
            {
                return null;
            }
        }

        public bool _UpdateTestType()
        {
            return clsTestTypeDataAccess.UpdateTestType((int)this.ID, this.TestTypeFees, this.TestTypeTitle, this.TestTypeDescription);
        }

        [Obsolete("⚠️ Warning: This method is not yet implemented. It will only return -1.", false)]
        public bool AddNewTestType()
        {
            throw new NotImplementedException("⚠️ Warning: This method is not yet implemented. It will only return -1.");
            //return clsTestTypeDataAccess.AddNewTestType(this.TestTypeTitle,this.TestTypeDescription,this.TestTypeFees);
        }


        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:

                //if (AddNewApplicationType())
                //{
                //    this._Mode = enMode.Update;
                //    return true;
                //}
                //else
                //    return false;


                case enMode.Update: return _UpdateTestType();

                default: return false;

            }

        }


    }
}