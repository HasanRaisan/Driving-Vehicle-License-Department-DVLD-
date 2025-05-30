using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.International_Applications
{
    public partial class FormShowInternatonalLicenseInfo : Form
    {
        public FormShowInternatonalLicenseInfo(int internationalLicnesneID)
        {
            InitializeComponent();
            this.userControlShowInternationalLicenseDetails1.SetLicenseID(internationalLicnesneID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
