using System;
using System.Windows.Forms;
using DVLD.Applications;
using DVLD.Driving_Licenses_Services;
using DVLD.International_Applications;
using DVLD.Mange_Applications;
using DVLD.Serveces;

namespace DVLD
{
    public partial class MainForm : Form
    {

        FormLoginScreen _formLoginScreen = new FormLoginScreen();
        public MainForm(FormLoginScreen form)
        {
            this._formLoginScreen = form;
            InitializeComponent();     
        }

        //users
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMangeUsers mangeUsers = new FormMangeUsers();
            mangeUsers.ShowDialog();
        }
        private void showCurrentUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           FormShowUserDetails UserDatails = new FormShowUserDetails(clsGlobal.CurrentUser.UserID);
            UserDatails.ShowDialog();
            
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChangePassword ChangePassword = new FormChangePassword(clsGlobal.CurrentUser.UserID);
            ChangePassword.ShowDialog();
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _formLoginScreen.Show();
            this.Close();
        }



        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangePeople mangePeople = new MangePeople();
            mangePeople.ShowDialog();
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

        private void localLicenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddLocalLicense formLocalLicense = new FormAddLocalLicense();
            formLocalLicense.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangeLoacalDrivingLicenseApplications mange = new MangeLoacalDrivingLicenseApplications();
            mange.ShowDialog();
        }

        // drivers
        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDrivers formDrivers = new FormDrivers();
            formDrivers.ShowDialog();
        }
        private void internationlDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formInternational = new FormAddNewInternationlalLicense();
            formInternational.ShowDialog();
        }


        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formRenewDrivngLicense = new FormRenwedLicense();
            formRenewDrivngLicense.ShowDialog();
        }

        private void remplacementDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var showReplacement = new FormReplacementForDamagedOrLostLicenses();
            showReplacement.ShowDialog();
        }

        private void detainDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var showDetain = new FormDetainLicense();
            showDetain.ShowDialog();

        }

        private void detainDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var showDetain = new FormDetainLicense();
            showDetain.ShowDialog();
        }

        private void rleaseDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowRelease = new FormReleaseLicense();
            ShowRelease.ShowDialog();
        }

        private void releaseDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowRelease = new FormReleaseLicense();
            ShowRelease.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var MangeLocalApplication = new MangeLoacalDrivingLicenseApplications();
            MangeLocalApplication.ShowDialog();
        } 


        private void detainDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowDetains = new FormManageDetainLicense();
            ShowDetains.ShowDialog();
        }

        private void detainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ManageApplications = new FormManageDetainLicense();
            ManageApplications.ShowDialog();
        }

        private void newDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
