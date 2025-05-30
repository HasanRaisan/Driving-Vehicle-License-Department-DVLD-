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
    public class clsTestTypes
    {

        public decimal TestTypeFees { get; set; }
        public int TestTypesID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }



        static public DataTable GetAllTestType()
        {
            return clsTestTypeDataAccess.GetAllTestType();
        }




        //   TestTypes
        //   _TestTypeID
        //   TestTypeTitle
        //   TestTypeDescription
        //   TestTypeFees      #deciaml


      public  clsTestTypes() { }


        clsTestTypes(int TestTypesID,decimal TestTypeFees,  string TestTypeTitle,string TestTypeDescription)
        {
            this.TestTypesID = TestTypesID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeFees = TestTypeFees;
            this.TestTypeDescription = TestTypeDescription;
        }

        static public clsTestTypes FindTestType(int TestTypeID)
        {
            string TestTypeTitle = null;
            string TestTypeDescription = null;
            decimal TestTypeFees = 0; 

            if(clsTestTypeDataAccess.FindTestType(TestTypeID,ref TestTypeFees,ref TestTypeTitle,ref TestTypeDescription))
            {
                return new clsTestTypes(TestTypeID, TestTypeFees, TestTypeTitle, TestTypeDescription);
            }
            else
            {
                return null;
            }
        }

        public bool _UpdateTestType()
        {
            return clsTestTypeDataAccess.UpdateTestType(this.TestTypesID,this.TestTypeFees,this.TestTypeTitle,this.TestTypeDescription);
        }

        public bool Save()
        {
            return _UpdateTestType(); ;
        }


    }


}