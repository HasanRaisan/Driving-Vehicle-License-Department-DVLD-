using BusinessLayer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DVLD.Mange_Applications
{
    public partial class FormAddUpdateApointment : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;


        int _TestTypeID = -1; 

        private clsLocalDrivingLicensesApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointmets _clsTestAppointment;
        private int _TestAppointmentID = -1;

        clsTestTypes clsTestType;

        public FormAddUpdateApointment( int LDLAppID, int TestTypeID , int AppointmentID = -1)
        {
            InitializeComponent();
            this._LocalDrivingLicenseApplicationID = LDLAppID;
            this._TestTypeID = TestTypeID;
            this._TestAppointmentID = AppointmentID;
        }

        private void SetGenralControls()
        {
            TestTypeInfo();
        }
        private void TestTypeInfo()
        {
            //  1- picture   


            // 2- group box name
            switch (this._TestTypeID)
            {
                case 1:
                    pictureBox1.Image = Resources.eye;
                    groupBoxMain.Text = "Vision Test";
                    break;
                case 2:
                    pictureBox1.Image = Resources.text_message;
                    groupBoxMain.Text = "Written Test";

                    break;
                case 3:
                    pictureBox1.Image = Resources.road_closed_sign;
                    groupBoxMain.Text = "Street Test";

                    break;   
                default:
                    break;
            }

        }


        private void LoadDate()
        {
            if (_TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
            
            this._LocalDrivingLicenseApplication = clsLocalDrivingLicensesApplication.FindLocalDrvingLicenseAppByID(this._LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))

                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationTypes.FindApplication(7).ApplicationFees.ToString();
                groupBoxRetakeTestInfo.Enabled = true;
                lblHeadLine.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                groupBoxRetakeTestInfo.Enabled = false;
                lblHeadLine.Text = "Schedule Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }

            lblLDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDLClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblApplicantPersonFullName.Text = _LocalDrivingLicenseApplication.PersonInfo.FullName();

            //lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestTypes.FindTestType(_TestTypeID).TestTypeFees.ToString();
                dateTimePickerDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                this.Text = "Add Appointment";
                _clsTestAppointment = new clsTestAppointmets();
            }

            else
            {

                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (Convert.ToDecimal(lblFees.Text) + Convert.ToDecimal(lblRetakeAppFees.Text)).ToString();

            if (!_HandleLockecAppointment())
                return;
            if (!_HandlePrviousTest())
                return;
        }

        private bool _HandlePrviousTest()
        {


            switch (_TestTypeID)
            {
                case 1:
                    //in this case no required prvious test to pass.
                    lblMessage.Visible = false;

                    return true;

                case 2:
                    //we check if pass visiontest 1.
                    if (!_LocalDrivingLicenseApplication.DidPassThisTestType(1))
                    {
                        lblMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblMessage.Visible = true;
                        btnSave.Enabled = false;
                        dateTimePickerDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMessage.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePickerDate.Enabled = true;
                    }
                    return true;

                case 3:

                    //we check if pass Written 2.
                    if (!_LocalDrivingLicenseApplication.DidPassThisTestType(2))
                    {
                        lblMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblMessage.Visible = true;
                        btnSave.Enabled = false;
                        dateTimePickerDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMessage.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePickerDate.Enabled = true;
                    }
                    return true;

            }
            return true;

        }
        private bool _HandleLockecAppointment()
        {
            // chech if a locked appointment
            if (_clsTestAppointment.isLocked)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Person already sat for this test, appointment loacked.";
                dateTimePickerDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            else
                lblMessage.Visible = false;

            return true;
        }

        private bool _LoadTestAppointmentData()
        {
            _clsTestAppointment = clsTestAppointmets.FindTestAppointment(_TestAppointmentID);

            if (_clsTestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _clsTestAppointment._PaidFees.ToString();
            this.Text = "Update Appointment";
            this._TestTypeID = _clsTestAppointment._TestTypeID;
            this.TestTypeInfo();


            //we compare the current date with the appointment date to set the min date.
            if (DateTime.Compare(DateTime.Now, _clsTestAppointment._AppointmentDate) < 0)
                dateTimePickerDate.MinDate = DateTime.Now;
            else
                dateTimePickerDate.MinDate = _clsTestAppointment._AppointmentDate;

            dateTimePickerDate.Value = _clsTestAppointment._AppointmentDate;

            if (_clsTestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _clsTestAppointment.RetakeTestAppInfo._PaidFees.ToString();
                groupBoxRetakeTestInfo.Enabled = true;
                lblHeadLine.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _clsTestAppointment.RetakeTestApplicationID.ToString();

            }
            return true;
        }


        private bool _SaveRetakeApplication()
        {

            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {

                var Application = new clsApplications();

                Application._ApplicantPersonID = _LocalDrivingLicenseApplication.PersonInfo._PersonID;
                Application._ApplicationDate = DateTime.Now;
                Application._ApplicationTypeID = 7;
                Application._LastStatusDate = DateTime.Now;
                Application._PaidFees = clsApplicationTypes.FindApplication(7).ApplicationFees;
                Application._CreatedByUserID = clsGlobal.CurrentUser.UserID;
                Application._LicenseClassID = _LocalDrivingLicenseApplication.LicenseClassID;

                if (!Application.Save())
                {
                    _clsTestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _clsTestAppointment.RetakeTestApplicationID = Application._ApplicationID;
                this.lblRetakeTestAppID.Text = Application._ApplicationID.ToString();
            }
            return true;
        }

        private void groupBoxMain_Enter(object sender, EventArgs e)
        {

        }
        private void FormAddApointment_Load(object sender, EventArgs e)
        {
            SetGenralControls();
            LoadDate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {


            if (!_SaveRetakeApplication())
                return;

            _clsTestAppointment._TestTypeID = _TestTypeID;
            _clsTestAppointment._LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _clsTestAppointment._AppointmentDate = dateTimePickerDate.Value;
            _clsTestAppointment._PaidFees = Convert.ToDecimal(lblFees.Text);
            _clsTestAppointment._CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_clsTestAppointment.Save())
            {
                _Mode = enMode.Update;
                this.Text = "Update Appointment";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePickerDate.Enabled = false;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void lblHeadLine_Click(object sender, EventArgs e)
        {

        }
    }
}
