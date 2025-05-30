using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer; 

namespace DVLD
{
    public partial class FormEditApplicationsType : Form
    {
        int ApplicationTypeID = -1;
        clsApplicationTypes clsApplicationType;

        public FormEditApplicationsType(int ApplicationTypeID)
        {
            InitializeComponent();
            this.ApplicationTypeID = ApplicationTypeID;
        }

        private void FormEditApplications_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            clsApplicationType = clsApplicationTypes.FindApplication(this.ApplicationTypeID);

            if (clsApplicationType != null)
            {
                lblIDValue.Text = clsApplicationType.ApplicationID.ToString();
                txtFees.Text = clsApplicationType.ApplicationFees.ToString();
                txtTitle.Text = clsApplicationType.ApplicationTypeTitle.ToString();
            }
            else
            {
                MessageBox.Show( "The application type could not be found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }






        private void lblHeadLine_Click(object sender, EventArgs e)
        {

        }

    

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            clsApplicationType.ApplicationTypeTitle = txtTitle.Text;
            clsApplicationType.ApplicationFees = Convert.ToDecimal(txtFees.Text);

            if (clsApplicationType.Save())
            {
                MessageBox.Show("The application type data has been saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("The application type data could not be saved. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void txtTital_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            };
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            };
            

            if (!clsValidatoin.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };
        }
    }
}
