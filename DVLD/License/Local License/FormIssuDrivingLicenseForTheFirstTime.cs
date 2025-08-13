using BusinessLayer;
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
    public partial class FormIssuDrivingLicenseForTheFirstTime : Form
    {
        int LDLAppID = -1;

      private  clsLocalDrivingLicensesApplication _LocalDrivingLicenseApplication;


        public FormIssuDrivingLicenseForTheFirstTime(int LDLAppID)
        {

            InitializeComponent();
            this.LDLAppID = LDLAppID;
            this.UserConShowBasicApplicationInfo1.SetLDLAppID(LDLAppID);
        }



        private void FormIssuDrivingLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            this.txtNotes.Focus();         
            this.btnClose.Visible = false;


            this._LocalDrivingLicenseApplication = clsLocalDrivingLicensesApplication.FindLocalDrvingLicenseAppByID(this.LDLAppID);

            if (_LocalDrivingLicenseApplication == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplication.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show("Cannot issue the driver's license. The Application must pass the whole tests", "License Issue Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            this.UserConShowBasicApplicationInfo1.SetLDLAppID(LDLAppID);

        }


        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(_LocalDrivingLicenseApplication.IssueLicenseForTheFirtTime(txtNotes.Text, clsGlobal.CurrentUser.UserID) != -1)
            {
                
                MessageBox.Show("The driver's license has been successfully issued.", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnIssue.Visible = false;
                this.btnCancel.Visible = false;
                this.btnClose.Visible = true;
                this.txtNotes.Enabled = false;

                this.UserConShowBasicApplicationInfo1.LoadData();
            }
            else
            {
                MessageBox.Show("Failed to issue the driver's license.", "License Issue Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

            

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
