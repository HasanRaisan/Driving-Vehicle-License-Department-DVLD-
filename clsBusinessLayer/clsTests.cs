using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsTests
    {
      public int _TestID {  get; set; }  
      public int _TestAppointmentID { get; set; }
      public bool _TestResult { get; set; }
      public string _Notes { get; set; }
      public int _CreatedByUserID { get; set; }


        public   clsTests() { }


        private bool _AddNewTest()
        {
            int TestAppointmentID = int.MinValue;
            bool TestResult = false;
            string Notes = string.Empty;
            int CreatedByUserID = int.MinValue;

            this._TestID = clsTestsDataAccess.AddNewTest(this._TestAppointmentID, this._TestResult, this._Notes, this._CreatedByUserID);

            return (this._TestID != -1);

        }

        private clsTests(int TestID, int TestAppointment, bool TestResult, string Notes)
        {
            this._Notes = Notes;
            this._TestID = TestID;
            this._TestResult = TestResult;
            this._TestAppointmentID = TestAppointment;
        }


        public static clsTests FindTestByTestAppointment(int TestAppointmentID)
        {
            int TestID = int.MinValue;
            bool TestRsult = false;
            string Notes = string.Empty ;

            if(clsTestsDataAccess.FindTest(TestAppointmentID, ref TestID, ref TestRsult, ref Notes))
            {
                return new clsTests(TestID,TestAppointmentID,TestRsult, Notes);

            } else return null;
        }


        public bool Save()
        {
            return this._AddNewTest();
        }
        

    }
}
