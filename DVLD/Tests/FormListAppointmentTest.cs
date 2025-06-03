using BusinessLayer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Mange_Applications
{
    public partial class FormListAppointmentTest : Form
    {
        int LDLAppID = -1;
        int TestTypeID = -1;

        public FormListAppointmentTest(int LDLAppID, int TestTypeID)
        {
            this.LDLAppID = LDLAppID;
            this.TestTypeID = TestTypeID;
            InitializeComponent();
            this.UserConShowBasicApplicationInfo1.SetLDLAppID(LDLAppID);
        }


        private void _LoadTestTypeInfo()
        {
           //  2- headline name
           //  1- picture

            switch (this.TestTypeID)
            {
                case 1:
                    pictureBox1.Image = Resources.eye;
                    lblTestTypeName.Text = "Vision Test Appointments";
                    this.Text = lblTestTypeName.Text;
                    break; 
                case 2:
                    pictureBox1.Image = Resources.text_message;
                    lblTestTypeName.Text = "Written Test Appointments";
                    this.Text = lblTestTypeName.Text;
                    break;
                case 3:
                    pictureBox1.Image = Resources.road_closed_sign;
                    lblTestTypeName.Text = "Street Test Appointments";
                    this.Text = lblTestTypeName.Text;
                    break;
                default:
                    break;
            }

        }



        private void _LoadDataToDataGridView()
        {
            DataTable dtTestAppointments = clsTestAppointmets.GetAllApplicationAppointments(this.LDLAppID);
            dgvTestAppointments.DataSource = dtTestAppointments;

            if (dtTestAppointments.Rows.Count > 2) { lblRecords.Text = $"# Records: {dtTestAppointments.Rows.Count}."; }
            else { lblRecords.Text = $"# Record: {dtTestAppointments.Rows.Count}."; }
        }

        private void btnAddAnApointment_Click(object sender, EventArgs e)
        {

            if (clsTestAppointmets.DoseLocalDrivingLicenseHaveAnActiveAppointment(this.LDLAppID))
            {
                MessageBox.Show("This application already has an active test appointment.", "Appointment Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (clsLocalDrivingLicensesApplication.DidPassThisTestType(this.LDLAppID, this.TestTypeID))
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                    FormAddUpdateApointment addApointment = new FormAddUpdateApointment(this.LDLAppID,this.TestTypeID);
                addApointment.ShowDialog();
                _LoadDataToDataGridView();
        }

        private void FormSchdulApointmet_Load(object sender, EventArgs e)
        {
            _LoadTestTypeInfo();
            _LoadDataToDataGridView();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTakeTest formTakeTest = new FormTakeTest((int) dgvTestAppointments.CurrentRow.Cells[0].Value);
            formTakeTest.ShowDialog();
            this.UserConShowBasicApplicationInfo1.LoadData();
            btnAddAnApointment.Enabled = false;
            _LoadDataToDataGridView();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FormUpdate = new FormAddUpdateApointment(LDLAppID,TestTypeID,(int)dgvTestAppointments.CurrentRow.Cells[0].Value);
            FormUpdate.ShowDialog();
            _LoadDataToDataGridView();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
