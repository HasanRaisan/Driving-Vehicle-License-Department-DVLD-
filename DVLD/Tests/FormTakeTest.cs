using BusinessLayer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Mange_Applications
{
    public partial class FormTakeTest : Form
    {
        int AppointmentID = -1;

        public FormTakeTest( int AppointmentID)
        {
            InitializeComponent();

            this.AppointmentID = AppointmentID;

        }


        private void TestTypeInfo(int TestTypeID)
        {
              //  1- picture 2- group box name
            switch (TestTypeID)
            {
                case 1:
                    this.pbTestImage.Image = Resources.eye;
                    this.groupBoxMain.Text = "Vision Test";
                    break;
                case 2:
                    this.pbTestImage.Image= Resources.text_message;
                    this.groupBoxMain.Text = "Written Test";
                    break;
                case 3:
                    this.pbTestImage.Image = Resources.road_closed_sign;
                    this.groupBoxMain.Text = "Street Test";
                    break;

            }

        }


        private void LoadDate()
        {
            var clsTestAppointment = BusinessLayer.clsTestAppointment.FindTestAppointment(this.AppointmentID);
           this.TestTypeInfo((int)clsTestAppointment.TestTypeID);

            if (clsTestAppointment == null)
            {
                MessageBox.Show("Test Appointment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTestAppointment.isLocked)
            {
               var clsTest = BusinessLayer.clsTest.FindTestByTestAppointment(this.AppointmentID);

                if (clsTest != null)
                {
                    if (clsTest.TestResult)
                    { radioButtonPass.Checked = true; radioButtonFail.Enabled = false; }
                    else { radioButtonFail.Checked = true; radioButtonPass.Enabled = false; }

                    lblTestIDValue.Text = clsTest.TestID.ToString();
                    txtNotes.Text = clsTest.Notes;
                    lblFees.Text = clsTestAppointment.PaidFees.ToString();
                }
                btnSave.Enabled = false;
                lblTestAlreadyTakenMessage.Visible = true;
                txtNotes.Enabled = false;

            }


           var LocalDrvingLicenseApp = clsLocalDrivingLicensesApplication.FindLocalDrvingLicenseAppByID(clsTestAppointment.LocalDrivingLicenseApplicationID);


           lblLDLAppID.Text = LocalDrvingLicenseApp.LocalDrivingLicenseApplicationID.ToString();
           lblApplicantPersonFullName.Text = LocalDrvingLicenseApp.PersonInfo.FullName();
           lblDLClass.Text = LocalDrvingLicenseApp.LicenseClassInfo.ClassName;
           lblTestDateValue.Text = clsTestAppointment.AppointmentDate.ToString();

           if (!clsTestAppointment.isLocked) lblFees.Text = clsTestType.FindTestType((clsTestType.enTestType)clsTestAppointment.TestTypeID).TestTypeFees.ToString();

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTakeTest_Load(object sender, EventArgs e)
        {
            lblTestAlreadyTakenMessage.Visible = false;
            LoadDate();

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save the result for this test? You will not be able to modify it afterward.", "Save Test Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
            {

                var clsTest = new clsTest();

                clsTest.TestAppointmentID = this.AppointmentID;
                clsTest.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                clsTest.Notes = txtNotes.Text;
                clsTest.TestResult = radioButtonPass.Checked? true: false;

            


               /* 
                                 if(!clsTestAppointment.LockedTestAppointment(this.AppointmentID))
                {
                    MessageBox.Show("Failed to blocked the appointemt. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                 
               */

                if (clsTest.Save())
                {
                      MessageBox.Show("The result has been saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      this.Close();
                }
                else
                {
                      MessageBox.Show("Failed to save the result. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
                }





            }



        }
    }
}
