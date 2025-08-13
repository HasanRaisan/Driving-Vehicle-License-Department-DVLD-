using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;
using DVLD;
using DVLD.Properties;

namespace DVLD
{
    public partial class ucnAddEitedPerson : UserControl
    {

        private int PersonID = -1;
        clsPerson clsPerson;
        

        public ucnAddEitedPerson()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;

        }
        public void SetPersonID(int personID)
        {
            this.PersonID = personID;

        }

        private void ucnAddEitedPerson_Load(object sender, EventArgs e)
        {
            LoadComboBoxCountries();

            dtpDateOfBirth.CustomFormat = "dd/MMM/yyyy";
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);


            if (clsPerson.IsPersonExist(this.PersonID))
                LoadPersonInfoForEdit();
            else LoadDefualtData();


        }
        private void LoadDefualtData()
        {
            clsPerson = new clsPerson();
            lblHeadline.Text = "Add Person";
            cbCountry.SelectedIndex = cbCountry.FindString("Iraq");
            rbMale.Checked = true;
            pbPersonPicture.Image = Resources.person_man;
            linlRemovePicture.Visible = false;

    
        }
        private void LoadComboBoxCountries()
        {
            DataTable dtCountris = clsCountry.GetCountries();
            
            foreach ( DataRow c in dtCountris.Rows)
            {
                cbCountry.Items.Add(c["CountryName"]);
            }
        }




        private void RadioButtonChangeImage(object sender, EventArgs e)
        {             
            if(pbPersonPicture.ImageLocation == null)
            {
                  if (rbFemale.Checked) 
                  {
                      pbPersonPicture.Image = Resources.admin_female;
                  }
                  else
                  {
                      pbPersonPicture.Image = Resources.person_man;
                  }
            }
        }

        private void LoadPersonInfoForEdit()
        {
            clsPerson = clsPerson.FindPerson(PersonID);

            if (clsPerson != null)
            {
                lblHeadline.Text = "Edite Person";
                lblPersonIDValue.Text = PersonID.ToString();


                txbFirstName.Text = clsPerson._FirstName;
                txbAddress.Text = clsPerson._Address;

                if (clsPerson._Email != null)
                {
                    txbEmail.Text = clsPerson._Email;
                }
                txbSecondName.Text = clsPerson._SecondName;
                txbLastName.Text = clsPerson._LastName;
                txbThirdName.Text = clsPerson._ThirdName;
                txbPhone.Text = clsPerson._Phone;
                cbCountry.SelectedIndex = clsPerson._NationalityCountryID - 1;
                dtpDateOfBirth.Value = clsPerson._DateOfBirth;
                txbNationalNo.Text = clsPerson._NationalNo;

                if (clsPerson._Gendor == 1)
                {
                    rbMale.Checked = true;
                }
                else
                { 
                    rbFemale.Checked = true;
                }


                if (!string.IsNullOrEmpty(clsPerson._ImagePath))
                {
                    if (System.IO.File.Exists(clsPerson._ImagePath))
                        pbPersonPicture.Load(clsPerson._ImagePath);
                    else
                        MessageBox.Show($"Could not found image: {clsPerson._ImagePath}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pbPersonPicture.Image = null; linlRemovePicture.Visible = false;
                    if (clsPerson._Gendor == 1)
                    {
                        pbPersonPicture.Image = Resources.admin_female;
                    }
                    else
                    {
                        pbPersonPicture.Image =Resources.person_man;
                    }

                }


            }
        }


        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image

            if (clsPerson._ImagePath != pbPersonPicture.ImageLocation)
            {
                if (clsPerson._ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(clsPerson._ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pbPersonPicture.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonPicture.ImageLocation.ToString();
                    
                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonPicture.ImageLocation = SourceImageFile;
                        return true;
                       
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }

            return true;
        }

        private void DisableAllFields()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox || ctrl is ComboBox || ctrl is RadioButton 
                    || ctrl is DateTimePicker || ctrl is Button || ctrl is LinkLabel)
                {
                        ctrl.Enabled = false;
                }
            }
            
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (!_HandlePersonImage())return;
            if (!_HandelAllInputs()) return;


            clsPerson._FirstName = txbFirstName.Text.ToString();
            clsPerson._SecondName = txbSecondName.Text;
            clsPerson._ThirdName = txbThirdName.Text;
            clsPerson._LastName = txbLastName.Text;
            clsPerson._DateOfBirth = dtpDateOfBirth.Value;
            clsPerson._Phone = txbPhone.Text;
            clsPerson._NationalNo = txbNationalNo.Text;
            clsPerson._Address = txbAddress.Text;
            clsPerson._NationalityCountryID = clsCountry.Find(cbCountry.SelectedIndex+1).CountryID;
            clsPerson._Email = txbEmail.Text;
            if(pbPersonPicture.ImageLocation != null)
                clsPerson._ImagePath = pbPersonPicture.ImageLocation.ToString();

            clsPerson._Gendor = rbFemale.Checked ? (short)0 : (short)1;

            if (clsPerson.Save())
            {
                btnSave.Visible = false;
                DisableAllFields();

                if (PersonID == -1)
                {
                    MessageBox.Show("Person detail has saved successfully","Saving Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    if (SendPersonID != null)
                    SendPersonID.Invoke(clsPerson.PersonID);
                }
                else MessageBox.Show("Person detail has edited successfully","Saving Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            else MessageBox.Show("Person detail has failed", "Saving falied", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public delegate int DlSendPersonID(int PersonID);
        public DlSendPersonID SendPersonID;



        private void linlSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialogProfilePicture.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialogProfilePicture.FilterIndex = 1;
            openFileDialogProfilePicture.RestoreDirectory = true;
            //openFileDialogProfilePicture.CheckFileExists = true;
            //openFileDialogProfilePicture.CheckPathExists = true;

            if (openFileDialogProfilePicture.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialogProfilePicture.FileName;
                pbPersonPicture.Load(selectedFilePath);
                linlRemovePicture.Visible = true;
            }
        }


        private void TextBoxKeyPrassOnlyLettersAndControl(object sender, KeyPressEventArgs e)
        {

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void TextBoxKeyPrassDontStartWithSpace(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length == 0 && e.KeyChar == ' ') 
            {
                e.Handled = true;
            }
        }

        private void ChangeBackColorToRed(TextBox text)
        {
            text.BackColor = Color.FromArgb(255, 255, 145, 145);
        }
        private void ChangeBackColorToWhite(TextBox text)
        {
            text.BackColor = Color.White;

        }
        private bool _HandelAllInputs()
        {
            bool isCom = true;
            string TExtBoxName = string.Empty;
            if (txbAddress.Text == "" )
            {
                isCom = false;
                TExtBoxName = "Address";
                ChangeBackColorToRed(txbAddress);
            }
            else ChangeBackColorToWhite(txbAddress);


            if (txbNationalNo.Text == "")
            {
                isCom = false;
                TExtBoxName += ", National No.";
                ChangeBackColorToRed(txbNationalNo);
            }
            else ChangeBackColorToWhite(txbNationalNo);

            if (txbPhone.Text == "")
            {
                isCom = false;
                TExtBoxName += ", Phone";
                ChangeBackColorToRed(txbPhone);
            }
            else ChangeBackColorToWhite(txbPhone);

            if (txbFirstName.Text == "" )
            {
                isCom = false;
                TExtBoxName += ", First Name";
                ChangeBackColorToRed(txbFirstName);
            }
            else ChangeBackColorToWhite(txbFirstName);

            if (txbSecondName.Text == "")
            {
                isCom = false;
                TExtBoxName += ", Second Name";
                ChangeBackColorToRed(txbSecondName);
            }
            else ChangeBackColorToWhite(txbSecondName);

            if (txbThirdName.Text == "")
            {
                isCom = false;
                TExtBoxName += ", Third Name";
                ChangeBackColorToRed(txbThirdName);
            }
            else ChangeBackColorToWhite(txbThirdName);

            if (txbLastName.Text == "")
            {
                isCom = false;
                TExtBoxName += ", Last Name";
                ChangeBackColorToRed(txbLastName);
            }
            else ChangeBackColorToWhite(txbLastName);

           



            if (TExtBoxName.StartsWith(","))
            {  
                TExtBoxName = TExtBoxName.Remove(0,1);
            }

            if (!isCom)
            {    
                if (TExtBoxName.Contains(", "))
                {
                    MessageBox.Show($"{TExtBoxName} are requird.");
                }
                else
                {
                    MessageBox.Show($"{TExtBoxName} is requird");
                }
            }
           return isCom;
        }


        private void txbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(txbNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider.SetError(txbNationalNo, null);
            }

            string NationalNo = clsPerson != null ? clsPerson._NationalNo : "";

            if (NationalNo != txbNationalNo.Text && clsPerson.IsNationalNoExists(txbNationalNo.Text.Trim()))
            {
                errorProvider.SetError(txbNationalNo,"This nationl No. is already used");
                e.Cancel = true;

            }
            else
            {
                errorProvider.SetError(txbNationalNo,string.Empty);
                e.Cancel = false;

            }
        }   
        private void txbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txbEmail.Text.Length > 0)
            {
                if (!txbEmail.Text.Contains("@"))
                {
                    errorProvider.SetError(txbEmail, "This Email is not valid");
                }
                else
                {
                    errorProvider.SetError(txbEmail, string.Empty);
                }
            }
            else
            {
                errorProvider.SetError(txbEmail, string.Empty);
            }
        }            
        private void linlRemovePicture_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonPicture.ImageLocation = null;

            if (rbFemale.Checked)
                pbPersonPicture.Image = Resources.admin_female;
            else
                pbPersonPicture.Image = Resources.person_man;

            linlRemovePicture.Visible = false;

            if(clsPerson != null)
            clsPerson._ImagePath = "";           
        }
    }
}
