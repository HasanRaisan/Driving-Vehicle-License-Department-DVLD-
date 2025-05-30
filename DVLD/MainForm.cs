using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;
using DVLD.Applications;
using DVLD.Driving_Licenses_Services;
using DVLD.International_Applications;
using DVLD.Mange_Applications;
using DVLD.Serveces;

namespace DVLD
{
    public partial class MainForm : Form
    {

        clsUsers clsUser = new clsUsers();

        public MainForm(clsUsers clsUser)
        {
            this.clsUser = clsUser; 
            InitializeComponent();     
        }



        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangePeople mangePeople = new MangePeople();
            mangePeople.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMangeUsers mangeUsers = new FormMangeUsers();
            mangeUsers.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

  

        private void showCurrentUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           FormShowUserDetails UserDatails = new FormShowUserDetails(clsUser.UserID);
            UserDatails.ShowDialog();
            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChangePassword ChangePassword = new FormChangePassword(clsUser.UserID);
            ChangePassword.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {                                                                    
            this.Hide();
            FormLoginScreen formLoginScreen = new FormLoginScreen();    
            formLoginScreen.ShowDialog();

        }

        private void applicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormApplicationsTypes formApplications = new FormApplicationsTypes();
            formApplications.ShowDialog();
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormTestTypes formTestTypes = new FormTestTypes();
            formTestTypes.ShowDialog();
        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void localLicenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddLocalLicense formLocalLicense = new FormAddLocalLicense(clsUser.UserName);
            formLocalLicense.ShowDialog();
        }

        private void mangeApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangeLoacalDrivingLicenseApplications mange = new MangeLoacalDrivingLicenseApplications(clsUser.UserName);
            mange.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDrivers formDrivers = new FormDrivers();
            formDrivers.ShowDialog();
        }

        private void internationlDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formInternational = new FormAddNewInternationlalLicense(clsUser.UserName);
            formInternational.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formRenewDrivngLicense = new FormRenwedLicense(clsUser.UserName);
            formRenewDrivngLicense.ShowDialog();
        }

        private void remplacementDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var showReplacement = new FormReplacementForDamagedOrLostLicenses(clsUser.UserName);
            showReplacement.ShowDialog();
        }

        private void detainDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var showDetain = new FormDetainLicense(clsUser.UserName);
            showDetain.ShowDialog();

        }

        private void rleaseDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowRelease = new FormReleaseLicense(clsUser.UserName);
            ShowRelease.ShowDialog();
        }

        private void detainDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowDetains = new FormManageDetainLicense(clsUser.UserName);
            ShowDetains.ShowDialog();
        }

        private void detainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ManageApplications = new FormManageDetainLicense(clsUser.UserName);
            ManageApplications.ShowDialog();
        }

        private void detainDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var showDetain = new FormDetainLicense(clsUser.UserName);
            showDetain.ShowDialog();
        }

        private void releaseDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowRelease = new FormReleaseLicense(clsUser.UserName);
            ShowRelease.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var MangeLocalApplication = new MangeLoacalDrivingLicenseApplications(clsUser.UserName);
            MangeLocalApplication.ShowDialog();
        }
    }
}
