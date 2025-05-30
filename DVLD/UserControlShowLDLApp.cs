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

namespace DVLD
{
    public partial class UserControlShowLDLApp : UserControl
    {
        int LDLAppID = -1;
        int ApplicantPersonID = -1;

        public UserControlShowLDLApp()
        {
            InitializeComponent();
        }

        public void SetLDLAppID(int LDLAppID)
        {
            this.LDLAppID = LDLAppID;
        }
        
        private void LoadDate()
        {


            clsLocalDrivingLicenseApplicationsBusinessLayer clsLDLApp = clsLocalDrivingLicenseApplicationsBusinessLayer.FindLDLAppByAppID(this.ApplicantPersonID);
            clsApplicationsBusinessLayer clsApplication = new clsApplicationsBusinessLayer();
            clsViewsBusinessLayer clsLDLAppView = new clsViewsBusinessLayer();

            if (clsLDLApp == null)
            {
                return;
            }
            else
            {
                 clsApplication = clsApplicationsBusinessLayer.FindApplication(clsLDLApp.ApplicationID);
                if (clsApplication == null)
                {
                    return;
                }
                else
                {
                     clsLDLAppView = clsViewsBusinessLayer.FindLDLAppView(this.LDLAppID);
                    if (clsLDLAppView == null)
                    {
                        return; 
                    }
                }
            }
            

            LDLAppIDValue.Text = this.LDLAppID.ToString();
            lblPassedTestValue.Text = clsLDLAppView._PassedTestCount.ToString()+"/3";
            lblLicenseClassName.Text = clsLDLAppView._ClassName;

            lblAppIDValue.Text = clsApplication._ApplicationID.ToString();
            lblCreatedByValue.Text = clsUserBusinessLayer.FindUser(clsApplication._CreatedByUser).UserName;
            lblDateValue.Text = clsApplication._ApplicationDate.ToString(); 
            lblStatusDateValue.Text=clsApplication._LastStatusDate.ToString();
            lblStatus.Text = clsLDLAppView._Status;
            lblFees.Text = clsApplication._PaidFees.ToString();
            lblApplicantPerson.Text = clsLDLAppView._FullName.ToString();
            lblType.Text = clsApplicationTypeBusinessLayer.FindApplications(clsApplication._ApplicationTypeID).ApplicationTypeTitle;

            this.ApplicantPersonID = clsApplication._ApplicantPersonID;
        }

        private void UserControlShowL_Load(object sender, EventArgs e)
        {
            LoadDate();
        }

        private void lbViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormShowPersonDetails frmShoePersonDetails = new FormShowPersonDetails(this.ApplicantPersonID);
            frmShoePersonDetails.ShowDialog();
        }
    }
}
