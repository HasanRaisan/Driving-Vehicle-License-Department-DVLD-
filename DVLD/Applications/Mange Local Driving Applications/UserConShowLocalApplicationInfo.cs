using BusinessLayer;
using DVLD.Mange_Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class UserConShowLocalApplicationInfo : UserControl
    {
        int LDLAppID = -1;
        int ApplicantPersonID = -1;
        int _licenseID = -1;

        public UserConShowLocalApplicationInfo()
        {
            InitializeComponent();
        }

        public void SetLDLAppID(int LDLAppID)
        {
            this.LDLAppID = LDLAppID;
            LoadData();
        }


        public void LoadData()
        {
            try
            {
                // Find the local Application view details
                var clsLDLAppView = clsLocalDrivingLicenseViewsBusinessLayer.FindLDLAppView(this.LDLAppID);
                if (clsLDLAppView == null)
                {
                    MessageBox.Show("Local Application view not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Find the associated (base) Application 
                var clsApplication = clsApplications.FindApplication(clsLDLAppView.BaseApplicationID);
                if (clsApplication == null)
                {
                    MessageBox.Show("Application details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                llShowLicenseInfo.Enabled = (this._licenseID = clsLicenses.GetLicenseIDByApplicationID(clsApplication._ApplicationID)) != -1;



                // Bind data to UI controls
                BindDataToControls(clsApplication, clsLDLAppView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataToControls(clsApplications Application, clsLocalDrivingLicenseViewsBusinessLayer LocalAppView)
        {
            var usCulture = new CultureInfo("en-US"); 

            LDLAppIDValue.Text = this.LDLAppID.ToString();

            const byte TotalTest = 3;
            lblPassedTestValue.Text = $"{LocalAppView._PassedTestCount}/{TotalTest}"; 
            lblLicenseClassName.Text = LocalAppView._ClassName;

            lblAppIDValue.Text = Application._ApplicationID.ToString();
            lblCreatedByValue.Text = clsUsers.FindUser(Application._CreatedByUserID).UserName;
            lblDateValue.Text = Application._ApplicationDate.ToString("yyyy-MMMM-dd",usCulture);
            lblStatusDateValue.Text = Application._LastStatusDate.ToString("yyyy-MMMM-dd",usCulture);
            lblStatus.Text = LocalAppView._Status;


            lblFees.Text = Application._PaidFees.ToString("C",usCulture);
            lblApplicantPerson.Text = LocalAppView._FullName;
            lblType.Text = clsApplicationTypes.FindApplication(Application._ApplicationTypeID).ApplicationTypeTitle;

            this.ApplicantPersonID = Application._ApplicantPersonID;
        }

        private void UserConShowBasicApplicationInfo_Load(object sender, EventArgs e)
        {
            this.llShowLicenseInfo.Enabled = false;
        }

        private void lbViewPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormShowPersonDetails frmShoePersonDetails = new FormShowPersonDetails(this.ApplicantPersonID);
            frmShoePersonDetails.ShowDialog();
        }

        private void lbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormShowLicense formShowLicense = new FormShowLicense(this._licenseID);
            formShowLicense.Show();
        }
    }
}
