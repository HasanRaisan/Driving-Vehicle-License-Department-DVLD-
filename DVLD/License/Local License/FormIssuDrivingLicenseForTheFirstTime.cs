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

        clsLocalDrivingLicensesApplication LocalDrvingApplication;


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

            if (!UpdateApplicationStatus())
            {
                MessageBox.Show("Cannor issue the driver's license. The Application must pass the whole tests", "License Issue Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // need to handle if person have a license in this class
        }



        private int SaveDriver()
        {
            // check if this person is already driver.

            LocalDrvingApplication = clsLocalDrivingLicensesApplication.FindLocalDrvingLicenseAppByID(this.LDLAppID);     
            var CheckDriver = clsDrivers.FindDriverByPersonID(LocalDrvingApplication.PersonInfo._PersonID);

            if (CheckDriver != null) return CheckDriver._DriverID;

            clsDrivers clsDriver = new clsDrivers();
            clsDriver._CreatedByUserID  = clsGlobal.CurrentUser.UserID;
            clsDriver._PersonID = LocalDrvingApplication.PersonInfo._PersonID;

            if(clsDriver.Save()) {return clsDriver._DriverID; } else return -1;

                
        }

        private bool UpdateApplicationStatus()
        {

            // Find the local Application view details
            var clsLDLAppView = clsLocalDrivingLicenseViewsBusinessLayer.FindLDLAppView(this.LDLAppID);
            if (clsLDLAppView != null)
            {
                if (clsLDLAppView._PassedTestCount == 3)
                {          
                    // There are three tests, so if any application completes these tests, its status must be changed to "complete."    
                    clsApplications.UpdateApplicationStatus(clsLDLAppView.BaseApplicationID, 3);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsLicenses License = new clsLicenses();

            int DriverID = this.SaveDriver();

            if (DriverID == -1)
            {
                MessageBox.Show("Failed to issue the driver's license. Driver Wasn't saved", "License Issue Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!UpdateApplicationStatus())
            {
                MessageBox.Show("Failed to issue the driver's license. The Application must pass the whole tests", "License Issue Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            License._LicenseClassID = LocalDrvingApplication.LicenseClassInfo.LicenseClassID;
            License._DriverID = DriverID;
            License._ApplicationID = LocalDrvingApplication.BaseApplicationID;
            License._CreatedByUserID = clsGlobal.CurrentUser.UserID;
            License._PaidFees = LocalDrvingApplication.LicenseClassInfo.ClassFees; 
            License._IsActive = true;
            License.IssueReason = clsLicenses.enIssueReason.FirstTime;
            License._Notes = txtNotes.Text;
            License._ExpirationDate = DateTime.Now.AddYears(LocalDrvingApplication.LicenseClassInfo.DefaultValidityLength); 

            if (License.Save())
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
