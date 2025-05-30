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

namespace DVLD.Applications
{
    public partial class FormEditTestType : Form
    {

        int TestTypeID;
        clsTestTypes clsTestType = new clsTestTypes();


        public FormEditTestType(int TestTypeID)
        {
            this.TestTypeID = TestTypeID;
            InitializeComponent();

        }

        private void LoadData()
        {
            clsTestType = clsTestTypes.FindTestType(this.TestTypeID);

            if (clsTestType != null)
            {
                lblIDValue.Text = clsTestType.TestTypesID.ToString();
                txtTestFees.Text = clsTestType.TestTypeFees.ToString();
                txtTestTital.Text = clsTestType.TestTypeTitle.ToString();
                txtDescription.Text = clsTestType.TestTypeDescription.ToString();
            }
            else
            {
                MessageBox.Show("The test type could not be found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

            clsTestType.TestTypeTitle = txtTestTital.Text;
            clsTestType.TestTypeDescription = txtDescription.Text;
            clsTestType.TestTypeFees = Convert.ToDecimal(txtTestFees.Text);

            if (clsTestType.Save())
            {
                MessageBox.Show("The test type data has been saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("The test type data could not be saved. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormEditTestType_Load(object sender, EventArgs e)
        {
            LoadData();

        }



        private void txtTestTital_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestTital.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtTestTital, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTestTital, null);
            };
        }

        private void txtDescribtion_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            };
        }

        private void txtTestFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTestFees, null);

            };


            if (!clsValidatoin.IsNumber(txtTestFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtTestFees, null);
            };
        }
    }
}
