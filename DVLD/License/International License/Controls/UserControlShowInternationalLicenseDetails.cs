using BusinessLayer;
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
    public partial class UserControlShowInternationalLicenseDetails : UserControl
    {
        int InternationalLicenseID = -1;
        public UserControlShowInternationalLicenseDetails()
        {
            InitializeComponent();
        }

        public void SetLicenseID(int IntLienceseID) { this.InternationalLicenseID = IntLienceseID; }

        public Image LoadImageFromPath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                return null;

            return Image.FromFile(imagePath);
        }


        void LoadData()
        {


            var clsLicense = clsShowInternationalLicenseBusinessLayer.FindLicenseByID(this.InternationalLicenseID);

            if (clsLicense == null) { return; }

            lblApplicantPersonFullName.Text = clsLicense.FullName;
            lblDateOfBirth.Text = clsLicense.DateOfBirth.ToString("MMM dd, yyyy");
            lblDriverID.Text = clsLicense.DriverID.ToString();
            lblExpDate.Text = clsLicense.ExpirationDate.ToString("MMM dd, yyyy");
            lblIssueDate.Text = clsLicense.IssueDate.ToString("MMM dd, yyyy");
            lblIsActive.Text = clsLicense.IsActive;
            lblGender.Text = clsLicense.Gender.ToString();
            lblNationalNumber.Text = clsLicense.NationalNo.ToString();
            lblLicenseID.Text = clsLicense.LicenseID.ToString();
            lblApplicationID.Text = clsLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = clsLicense.InternationalLicenseID.ToString();

            var clsPerson = BusinessLayer.clsPerson.FindPerson(clsLicense.NationalNo);
            if (clsPerson == null) { return; }
            pictureBoxPersonPicture.Image = LoadImageFromPath(clsPerson._ImagePath);

        }
        public void LoadDefaultData()
        {
            lblInternationalLicenseID.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblApplicationID.Text = "[???]";
            lblApplicantPersonFullName.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblExpDate.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblGender.Text = "[???]";
            lblNationalNumber.Text = "[???]";
            if (pictureBox1.Image != null) pictureBox1.Image = null;

        }

        private void groupBoxIntLicenseInfo_Enter(object sender, EventArgs e)
        {

        }

        private void UserControlShowInternationalLicenseDetails_Load(object sender, EventArgs e)
        {
            
            LoadData();
        }
    }
}
