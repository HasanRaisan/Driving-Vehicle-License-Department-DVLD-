using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsTest
    {

       public enum enMode { AddNew = 0, Update = 1 };
       public enMode Mode = enMode.AddNew;


      public int TestID {  get; set; }  
      public int TestAppointmentID { get; set; }
      public bool TestResult { get; set; }
      public string Notes { get; set; }
      public int CreatedByUserID { get; set; }

      public clsTestAppointment TestAppointmentInfo { set; get; }

       
      public clsTest()
      {
          this.TestID = -1;
          this.TestAppointmentID = -1;
          this.TestResult = false;
          this.Notes = "";
          this.CreatedByUserID = -1;

          Mode = enMode.AddNew;

      }


      private bool _AddNewTest()
      {
            this.TestID = clsTestsDataAccess.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);

      }

        private clsTest(int TestID, int TestAppointment, bool TestResult, string Notes)
        {
            this.Notes = Notes;
            this.TestID = TestID;
            this.TestResult = TestResult;
            this.TestAppointmentID = TestAppointment;
            this.TestAppointmentInfo = clsTestAppointment.FindTestAppointment(TestAppointmentID);

            this.Mode = enMode.Update;
        }


        public static clsTest FindTestByTestAppointment(int TestAppointmentID)
        {
            int TestID = int.MinValue;
            bool TestRsult = false;
            string Notes = string.Empty ;

            if(clsTestsDataAccess.FindTest(TestAppointmentID, ref TestID, ref TestRsult, ref Notes))
            {
                return new clsTest(TestID,TestAppointmentID,TestRsult, Notes);

            } else return null;
        }



        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsDataAccess.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.Update:
                    //return ();

                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return false;
            }
        }

    }
}
