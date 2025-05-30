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
    public partial class UserControlShowReplacementApplicationDetails : UserControl
    {
        public UserControlShowReplacementApplicationDetails()
        {
            InitializeComponent();
        }
        public void SetOldLicenseIDAndUsername(int OldLicensID, string Username )
        {

            this.lblOldLicenseIDValue.Text = OldLicensID.ToString();
            this.lblCreatedByValue.Text = Username;

        }

        public void SetApplicationTypeID(int ApplicationsTypeID)
        {
            this.lblApplicationFeesValue.Text = clsApplicationTypes.FindApplication(ApplicationsTypeID).ApplicationFees.ToString();
        }



        public void LoadDefaultValue()
        {
            this.lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy" ,new CultureInfo("en-US"));
            this.lblReplacedLicenseIDValue.Text = "[???]";
            this.lblRAppID.Text = "[???]";
            this.lblOldLicenseIDValue.Text = "[???]";
            this.lblRAppID.Text = "[???]";
        }

        public void SetReplacementdAppAndLicenseID(int RenewedAppID, int NewLicnseID)
        {
            this.lblReplacedLicenseIDValue.Text = NewLicnseID.ToString();
            this.lblRAppID.Text = RenewedAppID.ToString();

            // the complete info form database 
            LoadAppInf(RenewedAppID);
        }

        void LoadAppInf(int RenewedAppID)
        {

        }




        private void UserControlShowReplacementApplicationDetails_Load(object sender, EventArgs e)
        {
            LoadDefaultValue();
        }
    }
}
