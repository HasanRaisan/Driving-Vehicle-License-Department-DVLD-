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
    public partial class UserControlReleaseLicense : UserControl
    {
        public UserControlReleaseLicense()
        {
            InitializeComponent();
        }

        const sbyte ApplicationTypeID = 5;
        decimal ApplicationFees = clsApplicationTypes.FindApplication(ApplicationTypeID).ApplicationFees;

        public void SetDetainInfoAndUsername( clsDetainLicense DetainInfo, string username)
        {
            lblCreatedBy.Text = username;

            lblDetainID.Text = DetainInfo.DetainID.ToString();
            lblFineFees.Text = DetainInfo.FineFees.ToString();
            lblLicenseID.Text = DetainInfo.LicenseID.ToString();
            lblTotalFees.Text = (DetainInfo.FineFees + ApplicationFees).ToString();
        }

        public void SetApplicationID(int AppID)
        {
            lblApplicationID.Text = AppID.ToString();
        }

        public void SetDefualtValue()
        {
            lblDetainID.Text = "[???]";
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
            lblFineFees.Text = "[???]";
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblTotalFees.Text = "[???]";
            lblApplicationID.Text = "[???]";
            lblLicenseID.Text = "[???]";

        }

        private void UserControlReleaseLicense_Load(object sender, EventArgs e)
        {
            SetDefualtValue();
        }

        private void gbDetainInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
