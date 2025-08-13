using BusinessLayer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class UserControlDrivingLicenseInfo : UserControl
    {
        int _LocalDrivingLicenseAppID = -1;
        int _LicenseID = -1;
        public clsLicenseDetailsView LicenseDetailsInfo;

        public UserControlDrivingLicenseInfo()
        {
            InitializeComponent();

        }

        public void SetLocalDrivingLicenseAppID(int LocalDrivingLicenseAppID)
        {
            this._LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;
        }

        public void SetLicenseID(int LicenseID) {
            this._LicenseID = LicenseID;
            LoadData();
        }


        void LoadData()
        {
            

            if (this._LicenseID == -1)
            {

                var clsApp = clsLocalDrivingLicensesApplication.FindLocalDrvingLicenseAppByID(this._LocalDrivingLicenseAppID);
                if (clsApp != null)
                {
                    this._LicenseID = clsLicense.GetLicenseIDByApplicationID(clsApp.ApplicationID);
                }
            }


             LicenseDetailsInfo = clsLicenseDetailsView.FindLicenseDetailsByLicenseID(this._LicenseID);

            if (LicenseDetailsInfo == null) { return; }

            lblApplicantPersonFullName.Text = LicenseDetailsInfo._FullName;
            lblClassName.Text = LicenseDetailsInfo._ClassName;
            lblDateOfBirth.Text = LicenseDetailsInfo._DateOfBirth.ToString("MMM dd, yyyy");
            lblDriverID.Text = LicenseDetailsInfo._DriverID.ToString();
            lblExpDate.Text = LicenseDetailsInfo._ExpirationDate.ToString("MMM dd, yyyy");  
            lblIssueDate.Text = LicenseDetailsInfo._IssueDate.ToString("MMM dd, yyyy"); 
            lblNotes.Text = LicenseDetailsInfo._Notes;
            lblISDetained.Text = LicenseDetailsInfo._IsDetained;
            lblIsActive.Text = LicenseDetailsInfo._IsActive;
            lblLicenseID.Text = LicenseDetailsInfo._LicenseID.ToString();
            lblGender.Text = LicenseDetailsInfo._Gender;
            lblNationalNumber.Text = LicenseDetailsInfo._NationalNo.ToString();
            lblIssueReason.Text = LicenseDetailsInfo.IssueReasonText;

            if (!string.IsNullOrEmpty(LicenseDetailsInfo.PersonInfo._ImagePath) && System.IO.File.Exists(LicenseDetailsInfo.PersonInfo._ImagePath))
                pbDriverPicture.Image = Image.FromFile(LicenseDetailsInfo.PersonInfo._ImagePath);
            else
            {
                if (LicenseDetailsInfo.PersonInfo._Gendor == 1)
                    pbDriverPicture.Image = Resources.person_man;
                else pbDriverPicture.Image = Resources.admin_female;
            }


        }
        public void LoadDefaultData()
        {
            lblApplicantPersonFullName.Text = "[???]";
            lblClassName.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text ="[???]";
            lblExpDate.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblNotes.Text = "[???]";
            lblISDetained.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblGender.Text = "[???]";
            lblNationalNumber.Text = "[???]";
            lblIssueReason.Text = "[???]";
            if (pbDriverPicture.Image != null) pbDriverPicture.Image = null;
            
        }

        private void UserControlDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
        }
    }
}
