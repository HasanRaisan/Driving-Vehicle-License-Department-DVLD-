using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Mange_Applications
{
    public partial class FormEditeLocalDrivingLicenseApplication : Form
    {
        clsLocalDrivingLicensesApplication clsLDLApp = new clsLocalDrivingLicensesApplication();
        int PersonID = -1;
        int LicenseClassID = -1;

        public FormEditeLocalDrivingLicenseApplication(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            clsLDLApp = clsLocalDrivingLicensesApplication.FindLocalDrvingLicenseAppByID(LocalDrivingLicenseID);
            this.PersonID = clsLDLApp.PersonInfo.PersonID;
            this.userControlShowPersonDetails1.SetPersonID(this.PersonID);
          
            
            // no need  for: this.userControlShowPersonDetails1.LoadPersonDetail();  
            
        }



        void LoadComboBoxLicenseClasses()
        {
            DataTable dtLicenseClasses = clsLicenseClasses.GetAllLicenseClasses();



            foreach (DataRow drLicenseClass in dtLicenseClasses.Rows)
            {

                cbLicenseClass.Items.Add(drLicenseClass["ClassName"].ToString());
            }
            cbLicenseClass.SelectedIndex = clsLDLApp.LicenseClassID -1;
        }

        private void btnClancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEditeLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            this.btnClose.Visible = false;
            LoadComboBoxLicenseClasses();
        }


        private bool _CheckMinimumAllowedAge()
        {
            var dateOfBirth = clsPerson.FindPerson(this.PersonID)._DateOfBirth;

            int applicantAge = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.Date < dateOfBirth.AddYears(applicantAge))
                applicantAge--;

            if (applicantAge < 18) return false;

            byte minimumAllowedAge = clsLicenseClasses.FindLicenseClass(this.LicenseClassID).MinimumAllowedAge;

            return applicantAge >= minimumAllowedAge;
        }


        private void Save_Click(object sender, EventArgs e)
        {

            this. LicenseClassID  = clsLicenseClasses.FindLicenseClass(cbLicenseClass.Text).LicenseClassID;


            if(!_CheckMinimumAllowedAge())
            {
                MessageBox.Show("Person's age not allowed for this license.","Not allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }



            if (clsLicense.isLicenseExistByPersonIDAndLicenseClassID(this.PersonID, this.LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }



            int LDLAppID = clsApplication.GetActiveApplicationIDForLicenseClass(this.PersonID, clsApplication.enApplicationType.NewDrivingLicense, this.LicenseClassID);
            if (LDLAppID != -1)
            {
                MessageBox.Show($"This Person has a new application in this class with ID [{LDLAppID}]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLicenseClass.Focus();

                return;
            }

            clsLDLApp.LicenseClassID = this.LicenseClassID;



            if (clsLDLApp.UpdateLicenseClass())
            {
                MessageBox.Show("The license class has been successfully changed.", "Change Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnCancel.Visible = false;
                this.btnSave.Visible = false;
                btnClose.Visible = true; ;

            }
            else
            {
                MessageBox.Show("Failed to change the license class. Please try again.", "Change Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userControlShowPersonDetails1_Load(object sender, EventArgs e)
        {

        }
    }
}
