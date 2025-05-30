using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class UserControlDetainInfo : UserControl
    {
        public UserControlDetainInfo()
        {
            InitializeComponent();
        }



        public void SetUsernameAndLicenseID(string username, int licenseId)
        {
            this.lblCreatedBy.Text = username;
            this.lblLicenseID.Text = licenseId.ToString();
            this.txtFineFees.Enabled = true;
        }
        public void SetFineFeesTextBoxDisEnabled()
        {
            this.txtFineFees.Enabled = false;
        }
        public void SetDetainID(int id)
        {
            this.lblDetainID.Text = id.ToString();
        }
        public void SetDefualtValue()
        {
            lblDetainID.Text = "[???]";
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
            txtFineFees.Text = string.Empty;
            txtFineFees.Enabled = false;
            lblLicenseID.Text = "[???]";
        }


        public decimal GetFineFeesValue()
        {
            if (string.IsNullOrEmpty(txtFineFees.Text))
                return -1;
            return Convert.ToDecimal(txtFineFees.Text);
        }

        private void UserControlDetainInfo_Load(object sender, EventArgs e)
        {
            SetDefualtValue();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void FocusTextFees()
        {
            txtFineFees.Focus();
        }
    }
}
