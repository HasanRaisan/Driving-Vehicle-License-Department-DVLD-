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

namespace DVLD
{
    public partial class UserControlShowInternationalLincenseApplicationDetails : UserControl
    {
        public UserControlShowInternationalLincenseApplicationDetails()
        {
            InitializeComponent();
        }

        public void SetLocalLicenseIDAndUsername(int LocalLicensID, string Username)
        {
            this.lblLocalLicenseIDValue.Text = LocalLicensID.ToString();
            this.lblCreatedByValue.Text = Username;
        }

        

        public void SetInternationalAppID(int InternationalAppID)
        {
            LoadAppInf(InternationalAppID);
        }

        void LoadAppInf(int InternationalAppID)
        {

        }

        public void LoadDefaultValue()
        {
            var culture = new CultureInfo("en-US");

            lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy",culture);
            lblIssueDateValue.Text = DateTime.Now.ToString("dd/MMM/yyyy", culture);
            lblExpDateValue.Text = DateTime.Now.ToString("dd/MMM/yyyy", culture);
            lblFeesValue.Text = clsApplicationTypeBusinessLayer.FindApplications(6).ApplicationFees.ToString();
            this.lblLocalLicenseIDValue.Text = "???";
        }

        private void UserControlShowInternationalLincenseApplicationDetails_Load(object sender, EventArgs e)
        {

            LoadDefaultValue();
        }
    }
}
