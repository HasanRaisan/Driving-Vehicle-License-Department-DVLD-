using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace DVLD.Driving_Licenses_Services
{
    public partial class FormAddLocalLicense : Form
    {
        int PersonID = -1;
        int LicenseClassID = -1;

        public FormAddLocalLicense()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormLocalLicense_Load(object sender, EventArgs e)
        {
            LoadDate();
        }
        private void LoadDate()
        {
            LoadComboBoxLicenseClasses();
            lblAppDateValue.Text = DateTime.Now.ToString("yyyy/MM/dd");
            lblUsernameValue.Text = clsGlobal.CurrentUser.UserName.ToString();

        }
        private void btnNext_Click(object sender, EventArgs e)
        {

            this.PersonID = userConShopPersonDetailWithFilter1.GetPersonID();

            if(this.PersonID ==-1)
            {
                MessageBox.Show("You must get person information first", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tabControl1.SelectedTab = tabPageLoginInfo;

        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            this.PersonID = userConShopPersonDetailWithFilter1.GetPersonID();

            if (this.PersonID == -1)
            {
                MessageBox.Show("You must get person information first", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        void LoadComboBoxLicenseClasses()
        {
            DataTable dtLicenseClasses = clsLicenseClasses.GetAllLicenseClasses();



            foreach (DataRow drLicenseClass in dtLicenseClasses.Rows)
            {

                cbLicenseClass.Items.Add(drLicenseClass["ClassName"].ToString());
            }
            cbLicenseClass.SelectedIndex = 2;
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




        private int SaveApplication()
        {
            clsApplications clsApplication = new BusinessLayer.clsApplications();

            clsApplication._ApplicantPersonID = this.PersonID;
            clsApplication._CreatedByUserID = clsGlobal.CurrentUser.UserID;
            clsApplication._ApplicationTypeID = 1; // local driving license ID in apps types
            clsApplication._PaidFees = (decimal) Convert.ToSingle(lblFees.Text); // local driving license fees (from apps types table)
            clsApplication._LicenseClassID = cbLicenseClass.SelectedIndex + 1;
            clsApplication.Save();
            
            return clsApplication._ApplicationID;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {

            this.LicenseClassID = clsLicenseClasses.FindLicenseClass(cbLicenseClass.Text).LicenseClassID;



            if (!_CheckMinimumAllowedAge())
            {
                MessageBox.Show("Person's age not allowed for this license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (clsLicenses.isLicenseExistByPersonIDAndLicenseClassID(this.PersonID, this.LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            int LDLAppID = clsApplications.GetActiveApplicationIDForLicenseClass(this.PersonID, 1,this.LicenseClassID);
            if (LDLAppID != -1) 
            {
                MessageBox.Show($"This Person has a new application in this class with ID [{LDLAppID}]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLicenseClass.Focus();

                return;
            }


            clsLocalDrivingLicensesApplication clsLDLA = new clsLocalDrivingLicensesApplication();
            int BaseApplicationID =  SaveApplication();
            clsLDLA.BaseApplicationID = BaseApplicationID;
            clsLDLA.LicenseClassID = this.LicenseClassID;

            if (clsLDLA.Save()) 
            {
                MessageBox.Show("The application has been saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.lblAppIDValue.Text = clsLDLA.LocalDrivingLicenseApplicationID.ToString();
                this.btnClose.BringToFront();
                this.btnCancel.Visible = false;
                this.cbLicenseClass.Enabled = false;
            }
            else
            {
                clsApplications.DeleteApplication(BaseApplicationID);
                MessageBox.Show("The application could not be saved. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void FormAddLocalLicense_Activated(object sender, EventArgs e)
        {
               this.userConShopPersonDetailWithFilter1.FilterFocus();
        }


    }
}
