namespace DVLD.Driving_Licenses_Services
{
    partial class FormAddLocalLicense
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHeadLine = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPagePersonlInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.userConShopPersonDetailWithFilter1 = new DVLD.UserConShopPersonDetailWithFilter();
            this.tabPageLoginInfo = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUsernameValue = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAppDateValue = new System.Windows.Forms.Label();
            this.cbLicenseClass = new System.Windows.Forms.ComboBox();
            this.lblAppIDValue = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblAppID = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPagePersonlInfo.SuspendLayout();
            this.tabPageLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeadLine
            // 
            this.lblHeadLine.AutoSize = true;
            this.lblHeadLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadLine.ForeColor = System.Drawing.Color.Red;
            this.lblHeadLine.Location = new System.Drawing.Point(587, 103);
            this.lblHeadLine.Name = "lblHeadLine";
            this.lblHeadLine.Size = new System.Drawing.Size(534, 32);
            this.lblHeadLine.TabIndex = 6;
            this.lblHeadLine.Text = "New Local Driving License Application";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(686, 942);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(333, 59);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPagePersonlInfo);
            this.tabControl1.Controls.Add(this.tabPageLoginInfo);
            this.tabControl1.Location = new System.Drawing.Point(102, 154);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1467, 782);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPagePersonlInfo
            // 
            this.tabPagePersonlInfo.Controls.Add(this.btnNext);
            this.tabPagePersonlInfo.Controls.Add(this.userConShopPersonDetailWithFilter1);
            this.tabPagePersonlInfo.Location = new System.Drawing.Point(4, 29);
            this.tabPagePersonlInfo.Name = "tabPagePersonlInfo";
            this.tabPagePersonlInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePersonlInfo.Size = new System.Drawing.Size(1459, 749);
            this.tabPagePersonlInfo.TabIndex = 0;
            this.tabPagePersonlInfo.Text = "Personal Info";
            this.tabPagePersonlInfo.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(1204, 672);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(217, 59);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // userConShopPersonDetailWithFilter1
            // 
            this.userConShopPersonDetailWithFilter1.Location = new System.Drawing.Point(21, 33);
            this.userConShopPersonDetailWithFilter1.Name = "userConShopPersonDetailWithFilter1";
            this.userConShopPersonDetailWithFilter1.Size = new System.Drawing.Size(1400, 638);
            this.userConShopPersonDetailWithFilter1.TabIndex = 0;
            // 
            // tabPageLoginInfo
            // 
            this.tabPageLoginInfo.Controls.Add(this.label3);
            this.tabPageLoginInfo.Controls.Add(this.lblUsernameValue);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox5);
            this.tabPageLoginInfo.Controls.Add(this.label2);
            this.tabPageLoginInfo.Controls.Add(this.lblAppDateValue);
            this.tabPageLoginInfo.Controls.Add(this.cbLicenseClass);
            this.tabPageLoginInfo.Controls.Add(this.lblAppIDValue);
            this.tabPageLoginInfo.Controls.Add(this.btnSave);
            this.tabPageLoginInfo.Controls.Add(this.lblConfirmPassword);
            this.tabPageLoginInfo.Controls.Add(this.lblPassword);
            this.tabPageLoginInfo.Controls.Add(this.lblUserName);
            this.tabPageLoginInfo.Controls.Add(this.lblAppID);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox4);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox3);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox2);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox1);
            this.tabPageLoginInfo.Location = new System.Drawing.Point(4, 29);
            this.tabPageLoginInfo.Name = "tabPageLoginInfo";
            this.tabPageLoginInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLoginInfo.Size = new System.Drawing.Size(1459, 749);
            this.tabPageLoginInfo.TabIndex = 1;
            this.tabPageLoginInfo.Text = "Login Info";
            this.tabPageLoginInfo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(412, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 29);
            this.label3.TabIndex = 18;
            this.label3.Text = "15";
            // 
            // lblUsernameValue
            // 
            this.lblUsernameValue.AutoSize = true;
            this.lblUsernameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsernameValue.Location = new System.Drawing.Point(412, 380);
            this.lblUsernameValue.Name = "lblUsernameValue";
            this.lblUsernameValue.Size = new System.Drawing.Size(52, 29);
            this.lblUsernameValue.TabIndex = 17;
            this.lblUsernameValue.Text = "???";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD.Properties.Resources.id;
            this.pictureBox5.Location = new System.Drawing.Point(312, 380);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(41, 42);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 16;
            this.pictureBox5.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 29);
            this.label2.TabIndex = 15;
            this.label2.Text = "License Class:";
            // 
            // lblAppDateValue
            // 
            this.lblAppDateValue.AutoSize = true;
            this.lblAppDateValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppDateValue.Location = new System.Drawing.Point(412, 188);
            this.lblAppDateValue.Name = "lblAppDateValue";
            this.lblAppDateValue.Size = new System.Drawing.Size(52, 29);
            this.lblAppDateValue.TabIndex = 14;
            this.lblAppDateValue.Text = "???";
            // 
            // cbLicenseClass
            // 
            this.cbLicenseClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClass.FormattingEnabled = true;
            this.cbLicenseClass.Location = new System.Drawing.Point(407, 256);
            this.cbLicenseClass.Name = "cbLicenseClass";
            this.cbLicenseClass.Size = new System.Drawing.Size(366, 28);
            this.cbLicenseClass.TabIndex = 13;
            // 
            // lblAppIDValue
            // 
            this.lblAppIDValue.AutoSize = true;
            this.lblAppIDValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppIDValue.Location = new System.Drawing.Point(412, 124);
            this.lblAppIDValue.Name = "lblAppIDValue";
            this.lblAppIDValue.Size = new System.Drawing.Size(52, 29);
            this.lblAppIDValue.TabIndex = 12;
            this.lblAppIDValue.Text = "???";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(579, 641);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(333, 67);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPassword.Location = new System.Drawing.Point(57, 380);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(149, 29);
            this.lblConfirmPassword.TabIndex = 3;
            this.lblConfirmPassword.Text = "Created By:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(58, 316);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(216, 29);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Application Fees:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(58, 188);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(211, 29);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "Application Date:";
            // 
            // lblAppID
            // 
            this.lblAppID.AutoSize = true;
            this.lblAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppID.Location = new System.Drawing.Point(57, 124);
            this.lblAppID.Name = "lblAppID";
            this.lblAppID.Size = new System.Drawing.Size(235, 29);
            this.lblAppID.TabIndex = 0;
            this.lblAppID.Text = "D.L. Application ID:";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD.Properties.Resources.id;
            this.pictureBox4.Location = new System.Drawing.Point(312, 316);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(41, 42);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD.Properties.Resources.id;
            this.pictureBox3.Location = new System.Drawing.Point(312, 252);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(41, 42);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.id;
            this.pictureBox2.Location = new System.Drawing.Point(312, 188);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.id;
            this.pictureBox1.Location = new System.Drawing.Point(312, 124);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // FormAddLocalLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1699, 1056);
            this.Controls.Add(this.lblHeadLine);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormAddLocalLicense";
            this.Text = "Local License";
            this.Load += new System.EventHandler(this.FormLocalLicense_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPagePersonlInfo.ResumeLayout(false);
            this.tabPageLoginInfo.ResumeLayout(false);
            this.tabPageLoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeadLine;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPagePersonlInfo;
        private System.Windows.Forms.Button btnNext;
        private UserConShopPersonDetailWithFilter userConShopPersonDetailWithFilter1;
        private System.Windows.Forms.TabPage tabPageLoginInfo;
        private System.Windows.Forms.Label lblAppIDValue;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblAppID;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbLicenseClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAppDateValue;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUsernameValue;
    }
}