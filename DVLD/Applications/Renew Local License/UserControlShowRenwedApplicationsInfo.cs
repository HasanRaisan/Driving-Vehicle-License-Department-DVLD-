using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applcations_user_controls
{
    public partial class UserControlShowRenwedApplicationsInfo : UserControl
    {
        public UserControlShowRenwedApplicationsInfo()
        {
            InitializeComponent();
        }


        const int ApplicationsTypeID = 2;

        decimal RenweAppFees = clsApplicationType.FindApplication(ApplicationsTypeID).ApplicationFees;

        public void SetOldLicenseIDAndUsername(int OldLicensID, string Username)
        {
            this.lblOldLicenseIDValue.Text = OldLicensID.ToString();
            this.lblCreatedByValue.Text = Username;



            var culture = new CultureInfo("en-US");

            var licenseClass = clsLicenseClasses.FindLicenseClass(clsLicense.FindLicense(OldLicensID).LicenseClassID);
            if (licenseClass != null) {  
                this.lblExpDateValue.Text = DateTime.Today.AddYears(licenseClass.DefaultValidityLength).ToString("dd/MMM/yyyy", culture);
                this.lblLicenseFees.Text = licenseClass.ClassFees.ToString();
                this.lblTotalFees.Text = (RenweAppFees + licenseClass.ClassFees).ToString();
            }


        }


        public void LoadDefaultValue()
        {
            var culture = new CultureInfo("en-US");

            lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", culture);
            lblIssueDateValue.Text = DateTime.Now.ToString("dd/MMM/yyyy", culture);
            lblApplicationFeesValue.Text = RenweAppFees.ToString();

            this.lblExpDateValue.Text = "[???]";
            this.lblLicenseFees.Text = "[???]";
            this.lblTotalFees.Text = "[???]";
            this.lblOldLicenseIDValue.Text = "[???]";
            this.lblRenwedLicenseIDValue.Text = "[???]";
            this.lblRAppID.Text = "[???]";

            this.txtNotes.Text = string.Empty;  


        }

        public void SetRenwedAppAndLicenseID(int RenewedAppID, int NewLicnseID)
        {
            this.lblRenwedLicenseIDValue.Text = NewLicnseID.ToString();
            this.lblRAppID.Text = RenewedAppID.ToString();

            // the complete info form database 
            LoadAppInf(RenewedAppID);
        }

        void LoadAppInf(int RenewedAppID)
        {

        }




        public string GetNotes()
        {
            return txtNotes.Text;
        }
        public void SetTxetBoxNotesNotEnabled()
        {
            txtNotes.Enabled = false;
        }

        private void UserControlShowRenwedApplicationsInfo_Load(object sender, EventArgs e)
        {
            LoadDefaultValue();
        }
    }
}
