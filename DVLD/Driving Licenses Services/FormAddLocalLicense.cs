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
        string Username = string.Empty;

        public FormAddLocalLicense(string Username)
        {
            InitializeComponent();
            this.Username = Username;
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
            lblUsernameValue.Text = Username.ToString();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            // better way than add user   (-: * :-) 


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
            DataTable dtLicenseClasses = clsLicenseClassesBusinnessLayer.GetAllLicenseClasses();



            foreach (DataRow drLicenseClass in dtLicenseClasses.Rows)
            {

                cbLicenseClass.Items.Add(drLicenseClass["ClassName"].ToString());
            }
            cbLicenseClass.SelectedIndex = 2;
        }


        private int DosePersonHasTheSameAppliction()
        {
            DataTable dtPerson = clsApplicationsBusinessLayer.GetPersonApplications(this.PersonID);

            foreach (DataRow drApplication in dtPerson.Rows)
            {
                if ((int)drApplication["LicenseClassID"] == cbLicenseClass.SelectedIndex + 1 && (byte)drApplication["ApplicationStatus"] == 1)
                {
                    return (int)drApplication["ApplicationID"];
                }
            }
            return -1;

        }

        private int SaveApplication()
        {
            clsApplicationsBusinessLayer clsApplication = new BusinessLayer.clsApplicationsBusinessLayer();

            clsApplication.ApplicantPersonID = this.PersonID;
            clsApplication.CreatedByUser = clsUserBusinessLayer.FindUser(this.Username).UserID.ToString();
            clsApplication.ApplicationTypeID = 1; // local driving license ID in apps types
            clsApplication.PaidFees = 15; // local driving license fees (from apps types table)
            clsApplication.LicenseClassID = cbLicenseClass.SelectedIndex+1;
            clsApplication.Save();
            
            return clsApplication.ApplicationID;



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int AppID = DosePersonHasTheSameAppliction();
            if (AppID != -1) 
            {
                MessageBox.Show($"This Person has a new application in this class with ID [{AppID}]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            clsLocalDrivingLicenseApplicationsBusinessLayer clsLDLA = new clsLocalDrivingLicenseApplicationsBusinessLayer();
            clsLDLA.ApplicationID = SaveApplication();
            clsLDLA.LicenseClassID = cbLicenseClass.SelectedIndex+1;

            if (clsLDLA.Save())
            {
                MessageBox.Show("The application has been saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.lblAppIDValue.Text = clsLDLA.ApplicationID.ToString();
            }
            else
            {
                MessageBox.Show("The application could not be saved. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
